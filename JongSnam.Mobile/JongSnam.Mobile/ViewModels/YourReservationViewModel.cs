using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourReservationViewModel : BaseViewModel
    {
        public ObservableCollection<ReservationDto> Items { get; }

        private readonly IReservationServices _reservationServices;

        public Command SearchReservationCommand { get; }
        public Command DayGraphCommand { get; }
        public Command MonthGraphCommand { get; }
        public Command YearGraphCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command<ReservationDto> ItemTapped { get; }

        public YourReservationViewModel()
        {
            Title = "การจองของคุณ";

            _reservationServices = DependencyService.Get<IReservationServices>();

            Items = new ObservableCollection<ReservationDto>();

            SearchReservationCommand = new Command(OnSearchReservation);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<ReservationDto>(OnItemSelected);

            DayGraphCommand = new Command(async () => await OnDayGraph(Items));

            MonthGraphCommand = new Command(async () => await OnMonthGraph(Items));

            YearGraphCommand = new Command(async () => await OnYearGraph(Items));


        }
        async Task OnDayGraph(ObservableCollection<ReservationDto> items)
        {

            await Shell.Current.Navigation.PushAsync(new DayGraphPage(items));
        }

        async Task OnMonthGraph(ObservableCollection<ReservationDto> items)
        {

            await Shell.Current.Navigation.PushAsync(new MonthGraphPage(items));
        }

        async Task OnYearGraph(ObservableCollection<ReservationDto> items)
        {

            await Shell.Current.Navigation.PushAsync(new YearGraphPage(items));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _reservationServices.GetYourReservation(4, 1, 5);
                foreach (var item in items)
                {
                    if (item.ApprovalStatus == true)
                    {
                        Items.Add(
                            new YourReservationModel
                            {
                                Id = item.Id,
                                UserName = item.UserName,
                                StoreName = item.StoreName,
                                ContactMobile = item.ContactMobile,
                                StartTime = item.StartTime,
                                StopTime = item.StopTime,
                                ApprovalStatusString = "อนุมัติ"
                            });
                    }
                    else
                    {
                        Items.Add(
                            new YourReservationModel
                            {
                                Id = item.Id,
                                UserName = item.UserName,
                                StoreName = item.StoreName,
                                ContactMobile = item.ContactMobile,
                                StartTime = item.StartTime,
                                StopTime = item.StopTime,
                                ApprovalStatusString = "ไม่อนุมัติ"
                            });
                    }

                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async void OnItemSelected(ReservationDto reservationDto)
        {
            await Shell.Current.Navigation.PushAsync(new DetailYourReservationPage(reservationDto.Id.Value));
        }


        async void OnSearchReservation()
        {
            await Shell.Current.GoToAsync(nameof(SearchReservationPage));
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}