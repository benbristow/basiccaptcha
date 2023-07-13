using System.Net.Http;
using System.Threading.Tasks;

namespace BasicCaptcha.Providers;

public abstract class BaseProvider
{
    public abstract Task<bool> VerifyToken(string? token, HttpClient? httpClient = null);
}
