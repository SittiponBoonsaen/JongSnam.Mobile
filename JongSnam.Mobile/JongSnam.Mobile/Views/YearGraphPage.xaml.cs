using System.Collections.ObjectModel;
using JongSnam.Mobile.ViewModels;
using JongSnamService.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YearGraphPage : ContentPage
    {
        YearGraphViewModel _viewModel;
        public YearGraphPage(ObservableCollection<ReservationDto> items)
        {
            InitializeComponent();

            BindingContext = _viewModel = new YearGraphViewModel(items);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}