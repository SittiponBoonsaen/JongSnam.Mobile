using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultSearchItemPage : ContentPage
    {
        ResultSearchItemViewModel _viewModel;
        public ResultSearchItemPage(double startPrice, double toPrice, int? districtId, int? provinceId)
        {
            InitializeComponent();

            int? pro = provinceId == 0 || provinceId == null ? 0 : provinceId;
            if (pro == 0)
            {
                pro = null;
            }

            int? dis = districtId == 0 || districtId == null ? 0 : districtId;
            if (dis == 0)
            {
                dis = null;
            }

            BindingContext = _viewModel = new ResultSearchItemViewModel(startPrice, toPrice, pro, dis);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

    }
}