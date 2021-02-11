using JongSnam.Mobile.Validations;
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

        public ValidatableObject<string> Image { get; set; }
        public ValidatableObject<string> Name { get; set; }
        public ValidatableObject<string> Address { get; set; }
        public ValidatableObject<string> SubDistrict { get; set; }
        public ValidatableObject<string> District { get; set; }
        public ValidatableObject<string> Province { get; set; }
        public ValidatableObject<string> ContactMobile { get; set; }
        public ValidatableObject<string> Lat { get; set; }
        public ValidatableObject<string> Long { get; set; }
        public ValidatableObject<string> OfficeHours { get; set; }
        public ValidatableObject<string> Rules { get; set; }

        public void OnAppearing()
        {
            IsBusy = true;
        }
        private void InitValidation()
        {
            Image = new ValidatableObject<string>();
            Image.Validations.Add(new IsNullOrEmptyRule<string> {ValidationMessage = "Image is null"});
        }
        private bool IsValid
        {
            get
            {
                return Image.Validate();
            }
        }

        async void OnAlertYesNoClicked()
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            if (!answer || !IsValid)
            {
                return;
            }

            await Shell.Current.GoToAsync("..");
        }

    }
}
