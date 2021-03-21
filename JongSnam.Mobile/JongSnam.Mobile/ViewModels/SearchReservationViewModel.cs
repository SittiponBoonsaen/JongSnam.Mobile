using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class SearchReservationViewModel : BaseViewModel
    {
        public Command SearchCommand { get; }
        public Command CancelCommand { get; }
        public Command LoadItemsCommand { get; }

        public SearchReservationViewModel()
        {
            SearchCommand = new Command(OnSearch);
            CancelCommand = new Command(OnCancel);
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSearch()
        {

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
