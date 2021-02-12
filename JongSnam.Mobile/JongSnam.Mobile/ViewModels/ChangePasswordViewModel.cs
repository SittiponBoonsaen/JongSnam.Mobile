using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel
    { 
        public Command CancelCommand { get; }

        public ChangePasswordViewModel()
        {
            CancelCommand = new Command(OnCancel);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
