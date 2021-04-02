using System;

namespace JongSnam.Mobile.CustomErrors
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message)
        {
        }
    }
}
