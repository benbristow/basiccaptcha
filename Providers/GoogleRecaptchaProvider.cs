using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BasicCaptcha.Contracts;
using BasicCaptcha.Exceptions;
using Newtonsoft.Json;

namespace BasicCaptcha.Providers
{
    internal class GoogleRecaptchaProvider : IExternalCaptchaProvider
    {
        private readonly string _secretKey;

        private readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://www.google.com/recaptcha/api/")
        };

        internal GoogleRecaptchaProvider(string secretKey)
        {
            _secretKey = secretKey;
        }

        public Task<bool> VerifyToken(string token)
        {
            return !string.IsNullOrWhiteSpace(token)
                ? VerifyTokenInternal(token)
                : Task.FromResult(false);
        }

        private async Task<bool> VerifyTokenInternal(string token)
        {
            var parameters = new Dictionary<string, string>
            {
                { "secret", _secretKey },
                { "response", token }
            };

            try
            {
                var response = await _httpClient.PostAsync("siteverify", new FormUrlEncodedContent(parameters));
                var responseString = await response.Content.ReadAsStringAsync();
                var responseBody = JsonConvert.DeserializeObject<GoogleSiteVerifyResponse>(responseString);

                return responseBody?.Success ?? false;
            }
            catch (HttpRequestException)
            {
                throw new CaptchaException("Unable to contact Google RECAPTCHA service");
            }
        }
    }

    internal class GoogleSiteVerifyResponse
    {
        public bool Success { get; set; }
    }
}
