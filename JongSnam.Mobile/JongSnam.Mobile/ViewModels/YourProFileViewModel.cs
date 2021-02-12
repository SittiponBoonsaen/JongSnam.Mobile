using JongSnam.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourProFileViewModel : BaseViewModel
    {
        public Command ChangePasswordCommand { get; }

        public YourProFileViewModel()
        {
            ChangePasswordCommand = new Command(OnChangePassword);
        }

        async void OnChangePassword(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ChangePasswordPage));
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
