using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.Models
{
    public class YourFieldModel : FieldDto
    {
        public ImageSource ImageSource { get; set; }

        public ImageSource ImageSourceDF { get; set; }

        public string IsOpenString { get; set; }

        public string PriceString { get; set; }

        public StoreModel StoredtoModel { get; set; }
    }
}

