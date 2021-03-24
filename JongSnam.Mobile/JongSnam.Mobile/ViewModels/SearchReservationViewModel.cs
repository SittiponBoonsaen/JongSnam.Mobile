using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Views;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class SearchReservationViewModel : BaseViewModel
    {
        private string _userName;
        private string _storeName;

        public Command SearchCommand { get; }
        public Command LoadItemsCommand { get; }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }



        public SearchReservationViewModel()
        {

            SearchCommand = new Command(async () => await OnSearch(UserName, StoreName));

        }

        async Task OnSearch(string UserName, string StoreName)
        {
            await Shell.Current.Navigation.PushAsync(new ResultSearchYourReservationPage(UserName, StoreName));
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
