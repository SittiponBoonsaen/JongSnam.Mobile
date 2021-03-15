using System;
using System.Runtime.Serialization;

namespace JongSnam.Mobile.Services.Base
{
    [Serializable]
    internal class NoInternetConnectionException : Exception
    {
        public NoInternetConnectionException()
        {
        }

        public NoInternetConnectionException(string message) : base(message)
        {
        }

        public NoInternetConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoInternetConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}