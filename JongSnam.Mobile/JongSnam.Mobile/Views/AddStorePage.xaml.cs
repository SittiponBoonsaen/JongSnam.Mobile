using JongSnam.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddStorePage : ContentPage
    {
        AddStoreViewModel _viewModel;
        public AddStorePage(int userId)
        {
            InitializeComponent();
            BindingContext = _viewModel = new AddStoreViewModel(userId, map);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}