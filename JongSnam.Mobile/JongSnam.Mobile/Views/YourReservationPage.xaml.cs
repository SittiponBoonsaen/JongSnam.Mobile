using System;
using System.ComponentModel;
using JongSnam.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    public partial class YourReservationPage : ContentPage
    {
        YourReservationViewModel _viewModel;
        public YourReservationPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new YourReservationViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}