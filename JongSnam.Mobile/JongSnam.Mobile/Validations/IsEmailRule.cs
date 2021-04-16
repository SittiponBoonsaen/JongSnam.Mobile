using System.Text.RegularExpressions;

namespace JongSnam.Mobile.Validations
{
    public class IsEmailRule : IValidationRule<string>
    {
        private const string EMAILREGEX = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public string ValidationMessage { get; set; }

        public bool Check(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || (!string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email.ToLower(), EMAILREGEX)))
            {
                return true;
            }

            return false;
        }
    }
}
