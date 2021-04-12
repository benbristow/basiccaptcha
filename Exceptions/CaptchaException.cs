using System;

namespace BasicCaptcha.Exceptions
{
    public class CaptchaException : Exception
    {
        public CaptchaException(string message) : base(message)
        {
        }
    }
}