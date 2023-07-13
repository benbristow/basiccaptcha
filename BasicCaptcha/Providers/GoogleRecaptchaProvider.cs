using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BasicCaptcha.Exceptions;
using Newtonsoft.Json;

namespace BasicCaptcha.Providers;

public sealed class GoogleRecaptchaProvider : BaseProvider
{
    private readonly string _secretKey;

    public GoogleRecaptchaProvider(string secretKey)
    {
        _secretKey = secretKey;
    }

    public override Task<bool> VerifyToken(string? token, HttpClient? httpClient = null)
    {
        if (httpClient is null)
            throw new ArgumentNullException(nameof(httpClient), "HttpClient is required");

        return !string.IsNullOrWhiteSpace(token)
            ? VerifyTokenInternal(token, httpClient)
            : Task.FromResult(false);
    }

    private async Task<bool> VerifyTokenInternal(string token, HttpClient httpClient)
    {
        var parameters = new Dictionary<string, string>
        {
            { "secret", _secretKey },
            { "response", token },
        };

        try
        {
            var response = await httpClient.PostAsync(new Uri("https://www.google.com/recaptcha/api/siteverify"), new FormUrlEncodedContent(parameters));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<GoogleSiteVerifyResponse>(responseString);

            return responseBody?.Success ?? false;
        }
        catch (HttpRequestException e)
        {
            throw new CaptchaException("Unable to contact Google RECAPTCHA service", e);
        }
    }

    public sealed class GoogleSiteVerifyResponse
    {
        [JsonProperty]
        public bool Success { get; set; }
    }
}

