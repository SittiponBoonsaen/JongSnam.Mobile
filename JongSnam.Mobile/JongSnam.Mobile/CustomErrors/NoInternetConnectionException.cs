using System;

namespace JongSnam.Mobile.CustomErrors
{
    /// <summary>
    /// Customer error for no internet connection.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class NoInternetConnectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoInternetConnectionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoInternetConnectionException(string message) : base(message)
        {
        }
    }
}
