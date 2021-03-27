using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchReservationPage : ContentPage
    {
        SearchReservationViewModel _viewModel;
        public SearchReservationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SearchReservationViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}