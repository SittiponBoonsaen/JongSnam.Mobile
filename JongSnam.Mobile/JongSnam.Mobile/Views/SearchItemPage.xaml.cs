using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchItemPage : ContentPage
    {
        SearchItemViewModel _viewModel;
        public SearchItemPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SearchItemViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}