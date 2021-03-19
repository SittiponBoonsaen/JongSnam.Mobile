namespace JongSnam.Mobile.Validations
{

    public class IsSelectedItemRule<T> : IValidationRule<T>
    {
        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        /// <value>
        /// The validation message.
        /// </value>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Checks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The check result.
        /// </returns>
        public bool Check(T value)
        {
            if (value != null)
            {
                return true;
            }

            return false;
        }
    }
}
