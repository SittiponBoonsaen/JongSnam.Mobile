using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ResultSearchYourReservationViewModel : BaseViewModel
    {
        private readonly IReservationServices _reservationServices;

        public Command LoadItemsCommand { get; }

        public ObservableCollection<ReservationDto> Items { get; }

        public Command<ReservationDto> ItemTapped { get; }

        public ResultSearchYourReservationViewModel(string UserName, string StoreName, DateTime StartDate, DateTime EndDate)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();

            Items = new ObservableCollection<ReservationDto>();

            ItemTapped = new Command<ReservationDto>(OnItemSelected);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(UserName, StoreName, StartDate, EndDate));
        }
        async Task ExecuteLoadItemsCommand(string UserName, string StoreName, DateTime StartDate, DateTime EndDate)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                int startTimeYear = (int)StartDate.Year;
                int startTimeMonth = (int)StartDate.Month;
                int startTimeDay = (int)StartDate.Day;
                int stopTimeYear = (int)EndDate.Year;
                int stopTimeMonth = (int)EndDate.Month;
                int stopTimeDay = (int)EndDate.Day;


                var items = await _reservationServices.GetReservationBySearch(4, startTimeYear, startTimeMonth, startTimeDay, 12, 0, 0, stopTimeYear, stopTimeMonth, stopTimeDay, 12, 0, 0, UserName, StoreName, 1, 12);
                var data = items;
                foreach (var item in items)
                {
                    Items.Add(
                           new YourReservationModel
                           {
                               Id = item.Id,
                               UserName = item.UserName,
                               StoreName = item.StoreName,
                               ContactMobile = item.ContactMobile,
                               StartTimePicker = item.StartTime.Value.TimeOfDay,
                               StopTimePicker = item.StopTime.Value.TimeOfDay,
                               DateTime = item.StartTime.Value.Date
                           });
                }

            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        private async void OnItemSelected(ReservationDto reservationDto)
        {
            await Shell.Current.Navigation.PushAsync(new DetailYourReservationPage(reservationDto.Id.Value));
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
