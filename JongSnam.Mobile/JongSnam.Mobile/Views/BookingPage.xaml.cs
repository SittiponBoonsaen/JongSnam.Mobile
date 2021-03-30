using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.ViewModels;
using JongSnamService.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingPage : ContentPage
    {
        BookingViewModel _viewModel;
        public BookingPage(FieldDto fieldDto, string storeName)
        {
            InitializeComponent();
            BindingContext = _viewModel = new BookingViewModel(fieldDto, storeName);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}