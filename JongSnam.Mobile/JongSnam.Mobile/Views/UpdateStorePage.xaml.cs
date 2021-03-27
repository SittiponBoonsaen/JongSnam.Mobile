using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateStorePage : ContentPage
    {
        UpdateStoreViewModel _viewModel;
        public UpdateStorePage(int idStore)
        {
            InitializeComponent();

            BindingContext = _viewModel = new UpdateStoreViewModel(idStore);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}