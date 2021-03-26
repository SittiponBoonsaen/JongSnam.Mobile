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
    public partial class CommentPage : ContentPage
    {
        CommentViewModel _viewModel;
        public CommentPage(ReviewDto reviewDto)
        {
            InitializeComponent();
            BindingContext = _viewModel = new CommentViewModel(reviewDto);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}