using Microsoft.Extensions.DependencyInjection;
using SimpleCaptcha.Contracts;
using SimpleCaptcha.Enums;

namespace SimpleCaptcha.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCaptcha(
            this IServiceCollection services,
            ExternalCaptchaProvider externalCaptchaProvider,
            string secretKey)
        {
            services.AddTransient<ICaptchaService>(_ => new CaptchaService(externalCaptchaProvider, secretKey));
            return services;
        }
    }
}