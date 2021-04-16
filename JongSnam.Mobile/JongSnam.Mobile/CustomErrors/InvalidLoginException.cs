using System;

namespace JongSnam.Mobile.CustomErrors
{
    /// <summary>
    /// Invalid login exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidLoginException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidLoginException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidLoginException(string message) : base(message)
        {
        }
    }
}