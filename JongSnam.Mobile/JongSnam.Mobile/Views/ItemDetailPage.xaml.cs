using JongSnam.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace JongSnam.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}