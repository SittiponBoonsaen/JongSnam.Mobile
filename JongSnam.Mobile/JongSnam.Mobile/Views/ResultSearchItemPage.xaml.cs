using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultSearchItemPage : ContentPage
    {
        ResultSearchItemViewModel _viewModel;
        public ResultSearchItemPage(double startPrice, double toPrice, int districtId, int provinceId)
        {
            InitializeComponent();
            BindingContext = _viewModel = new ResultSearchItemViewModel(startPrice, toPrice, districtId, provinceId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}