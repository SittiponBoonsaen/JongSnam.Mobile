using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<ReservationDto> Items { get; }

        private readonly IReservationServices _reservationServices;

        public Command SearchReservationCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command<ReservationDto> ItemTapped { get; }

        public AboutViewModel()
        {
            Title = "การจองของคุณ";

            _reservationServices = DependencyService.Get<IReservationServices>();

            Items = new ObservableCollection<ReservationDto>();

            SearchReservationCommand = new Command(OnSearchReservation);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<ReservationDto>(OnItemSelected);

        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                //Items.Clear();
                //var items = await _reservationServices.GetYourReservation(1, 6);


                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}


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
            //await Shell.Current.Navigation.PushAsync(new ListFieldPage(storeDto));
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