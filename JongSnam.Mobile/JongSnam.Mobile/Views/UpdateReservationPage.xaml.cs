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
    public partial class UpdateReservationPage : ContentPage
    {
        UpdateReservationViewModel _viewModel;
        public UpdateReservationPage(int reservationId)
        {
            InitializeComponent();
            BindingContext = _viewModel = new UpdateReservationViewModel(reservationId);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}