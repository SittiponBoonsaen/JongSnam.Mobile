using JongSnam.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourStorePage : ContentPage
    {
        YourStoreViewModel _viewModel;

        public YourStorePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new YourStoreViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}