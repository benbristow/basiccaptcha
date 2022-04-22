using System.Threading.Tasks;
using BasicCaptcha.Contracts;

namespace BasicCaptcha.Providers
{
    internal class DummyProvider : IExternalCaptchaProvider
    {
        public Task<bool> VerifyToken(string token) =>
            Task.FromResult(token?.ToLowerInvariant().Trim() != "fail");
    }
}
