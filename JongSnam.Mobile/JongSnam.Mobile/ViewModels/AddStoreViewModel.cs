using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class AddStoreViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }

        public AddStoreViewModel()
        {
            SaveCommand = new Command(OnAlertYesNoClicked);
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
        async void OnAlertYesNoClicked()
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            if (!answer)
            {
                return;
            }

            await Shell.Current.GoToAsync("..");
        }

    }
}
