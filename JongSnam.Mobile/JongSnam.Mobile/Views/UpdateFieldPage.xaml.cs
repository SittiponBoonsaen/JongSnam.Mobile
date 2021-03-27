using JongSnam.Mobile.ViewModels;
using JongSnamService.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateFieldPage : ContentPage
    {
        UpdateFieldViewModel _viewModel;
        public UpdateFieldPage(FieldDto fieldDto)
        {
            InitializeComponent();
            BindingContext = _viewModel = new UpdateFieldViewModel(fieldDto);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}