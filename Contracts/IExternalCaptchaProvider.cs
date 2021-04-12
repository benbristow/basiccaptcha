using System.Threading.Tasks;

namespace BasicCaptcha.Contracts
{
    internal interface IExternalCaptchaProvider
    {
        public Task<bool> VerifyToken(string token);
    }
}