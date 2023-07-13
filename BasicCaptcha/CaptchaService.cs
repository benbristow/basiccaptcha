using System.Net.Http;
using System.Threading.Tasks;
using BasicCaptcha.Contracts;
using BasicCaptcha.Providers;

namespace BasicCaptcha;

public sealed class CaptchaService : ICaptchaService
{
    private readonly BaseProvider _provider;
    private readonly HttpClient? _httpClient;

    public CaptchaService(BaseProvider provider, HttpClient? httpClient = null)
    {
        _provider = provider;
        _httpClient = httpClient;
    }

    public Task<bool> VerifyTokenAsync(string? token) => _provider.VerifyTokenAsync(token, _httpClient);
}
