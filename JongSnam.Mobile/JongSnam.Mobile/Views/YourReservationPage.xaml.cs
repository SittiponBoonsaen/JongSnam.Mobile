using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;

namespace JongSnam.Mobile.Views
{
    public partial class YourReservationPage : ContentPage
    {
        YourReservationViewModel _viewModel;
        public YourReservationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new YourReservationViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}