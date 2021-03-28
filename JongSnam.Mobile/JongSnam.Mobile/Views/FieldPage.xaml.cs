using JongSnam.Mobile.ViewModels;
using JongSnamService.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldPage : ContentPage
    {
        FieldViewModel _viewModel;
        public FieldPage(FieldDto fieldDto, string StoreName)
        {
            InitializeComponent();
            BindingContext = _viewModel = new FieldViewModel(fieldDto, StoreName);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}