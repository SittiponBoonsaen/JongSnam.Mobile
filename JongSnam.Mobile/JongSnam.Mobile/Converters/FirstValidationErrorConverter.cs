using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace JongSnam.Mobile.Converters
{
    /// <summary>
    /// First validation error converter class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.IValueConverter" />
    public class FirstValidationErrorConverter : IValueConverter
    {
        /// <summary>
        /// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>
        /// Result converted.
        /// </returns>
        /// <exception cref="InvalidOperationException">The target must be a list of string</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is List<string>))
            {
                throw new InvalidOperationException("The target must be a list of string");
            }

            var errors = (List<string>)value;
            return errors.Count > 0 ? errors[0] : null;
        }

        /// <summary>
        /// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <exception cref="NotImplementedException">We don't implement.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
