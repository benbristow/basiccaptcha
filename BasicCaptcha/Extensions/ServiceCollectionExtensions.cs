using System.Net.Http;
using BasicCaptcha.Contracts;
using BasicCaptcha.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCaptcha.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCaptcha(
        this IServiceCollection services,
        BaseProvider provider)
    {
        services.AddTransient<ICaptchaService>(serviceProvider =>
            new CaptchaService(provider, serviceProvider.GetRequiredService<HttpClient>()));
        return services;
    }
}
