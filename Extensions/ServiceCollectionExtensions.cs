using BasicCaptcha.Contracts;
using BasicCaptcha.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCaptcha.Extensions
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