using JongSnam.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        ChangePasswordViewModel _viewModel;
        public ChangePasswordPage(int idUser)
        {
            InitializeComponent();
            BindingContext = _viewModel = new ChangePasswordViewModel(idUser);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}