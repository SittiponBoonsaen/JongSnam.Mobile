using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFieldPage : ContentPage
    {
        AddFieldViewModel _viewModel;
        public AddFieldPage(int storeId)
        {
            InitializeComponent();
            BindingContext = _viewModel = new AddFieldViewModel(storeId);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}