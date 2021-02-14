using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateFieldViewModel : BaseViewModel
    { 
        public Command DeleteFieldCommand { get; }
        public Command SaveCommand { get; }
        public UpdateFieldViewModel()
        {
            DeleteFieldCommand = new Command(OnDeleteFieldCommandAlertYesNoClicked);

            SaveCommand = new Command(OnSaveCommandAlertYesNoClicked);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
        async void OnDeleteFieldCommandAlertYesNoClicked()
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            if (!answer)
            {
                return;
            }

            await Shell.Current.GoToAsync("..");
        }

        async void OnSaveCommandAlertYesNoClicked()
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
