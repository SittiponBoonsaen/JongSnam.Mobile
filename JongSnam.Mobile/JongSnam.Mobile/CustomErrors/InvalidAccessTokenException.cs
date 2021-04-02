using System;

namespace JongSnam.Mobile.CustomErrors
{
    /// <summary>
    /// Invalid access token exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidAccessTokenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccessTokenException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidAccessTokenException(string message) : base(message)
        {
        }
    }
}