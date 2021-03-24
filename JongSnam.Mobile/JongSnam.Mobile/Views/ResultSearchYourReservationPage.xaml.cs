using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultSearchYourReservationPage : ContentPage
    {
        ResultSearchYourReservationViewModel _viewModel;
        public ResultSearchYourReservationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ResultSearchYourReservationViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}