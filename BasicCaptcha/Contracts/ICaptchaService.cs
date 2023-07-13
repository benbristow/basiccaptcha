using System.Threading.Tasks;

namespace BasicCaptcha.Contracts;

public interface ICaptchaService
{
    public Task<bool> VerifyTokenAsync(string? token);
}
