using System;
using System.Threading.Tasks;
using JongSnam.Mobile.Views;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class SearchReservationViewModel : BaseViewModel
    {
        private string _userName;
        private string _storeName;
        private DateTime _startdate;
        private DateTime _enddate;

        String myDate = DateTime.Now.ToString();

        public Command SearchCommand { get; }
        public Command LoadItemsCommand { get; }

        public DateTime StartDate
        {
            get => _startdate;
            set
            {
                _startdate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get => _enddate;
            set
            {
                _enddate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }


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

            SearchCommand = new Command(async () => await OnSearch(UserName, StoreName, StartDate, EndDate));

        }

        async Task OnSearch(string UserName, string StoreName, DateTime StartDate, DateTime EndDate)
        {
            await Shell.Current.Navigation.PushAsync(new ResultSearchYourReservationPage(UserName, StoreName, StartDate, EndDate));
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
