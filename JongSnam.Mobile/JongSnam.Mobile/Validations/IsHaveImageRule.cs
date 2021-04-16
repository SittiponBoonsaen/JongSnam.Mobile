using Xamarin.Forms;

namespace JongSnam.Mobile.Validations
{
    public class IsHaveImageRule : IValidationRule<ImageSource>
    {
        public string OriginalFile { get; set; }

        public string ValidationMessage { get; set; }

        public bool Check(ImageSource value)
        {
            var isValid = false;
            if (value != null)
            {
                var fileImageSource = value as FileImageSource;
                if (fileImageSource != null && fileImageSource.File != OriginalFile)
                {
                    isValid = true;
                }
                else if (value is StreamImageSource)
                {
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}
