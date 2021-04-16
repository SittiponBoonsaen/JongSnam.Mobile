using System;

namespace JongSnam.Mobile.CustomErrors
{
    /// <summary>
    /// Item exists exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ItemExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemExistsException" /> class
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ItemExistsException(string message) : base(message)
        {
        }
    }
}
