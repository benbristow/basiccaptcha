using System;
using System.Threading.Tasks;
using BasicCaptcha.Contracts;
using BasicCaptcha.Enums;
using BasicCaptcha.Providers;

namespace BasicCaptcha
{
    public class CaptchaService : ICaptchaService
    {
        private readonly IExternalCaptchaProvider _externalCaptchaProvider;

        public CaptchaService(ExternalCaptchaProvider externalCaptchaProvider, string secretKey)
        {
            _externalCaptchaProvider = externalCaptchaProvider switch
            {
                ExternalCaptchaProvider.GoogleRecaptcha => new GoogleRecaptcha(secretKey),
                _ => throw new ArgumentOutOfRangeException(nameof(externalCaptchaProvider), externalCaptchaProvider, null)
            };
        }

        public Task<bool> VerifyToken(string token) => _externalCaptchaProvider.VerifyToken(token);
    }
}