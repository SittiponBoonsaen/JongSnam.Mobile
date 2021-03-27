using JongSnam.Mobile.Models;
using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;

namespace JongSnam.Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}