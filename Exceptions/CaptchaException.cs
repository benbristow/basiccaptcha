using System;

namespace SimpleCaptcha.Exceptions
{
    public class CaptchaException : Exception
    {
        public CaptchaException(string message) : base(message)
        {
        }
    }
}