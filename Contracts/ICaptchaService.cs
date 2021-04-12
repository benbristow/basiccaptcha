using System.Threading.Tasks;

namespace SimpleCaptcha.Contracts
{
    public interface ICaptchaService
    {
        public Task<bool> VerifyToken(string token);
    }
}