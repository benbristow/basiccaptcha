using System;

namespace BasicCaptcha.Exceptions;

public sealed class CaptchaException : Exception
{
    public CaptchaException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
