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
    public partial class ReviewPage : ContentPage
    {
        ReviewViewModel _viewModel;
        public ReviewPage(int storeId)
        {
            InitializeComponent();
            BindingContext = _viewModel = new ReviewViewModel(storeId);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        async void StartCall(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new CommentPage());
        }
    }
}