using JongSnam.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourFieldPage : ContentPage
    {
        YourFieldViewModel _viewModel;
        public YourFieldPage(int storeId, string storeName)
        {
            InitializeComponent();
            BindingContext = _viewModel = new YourFieldViewModel(storeId, storeName);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearingAsync();
        }
    }
}