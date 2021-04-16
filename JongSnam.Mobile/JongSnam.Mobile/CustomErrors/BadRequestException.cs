using System;

namespace JongSnam.Mobile.CustomErrors
{
    /// <summary>
    /// Custom error bad request exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
