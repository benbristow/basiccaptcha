using System.Threading.Tasks;

namespace SimpleCaptcha.Contracts
{
    internal interface IExternalCaptchaProvider
    {
        public Task<bool> VerifyToken(string token);
    }
}