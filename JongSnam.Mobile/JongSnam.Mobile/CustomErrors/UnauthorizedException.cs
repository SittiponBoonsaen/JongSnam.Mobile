using System;

namespace JongSnam.Mobile.CustomErrors
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
