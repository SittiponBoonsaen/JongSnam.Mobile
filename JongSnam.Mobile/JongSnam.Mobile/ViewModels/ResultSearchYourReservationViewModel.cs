using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
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

        public ResultSearchYourReservationViewModel(string UserName, string StoreName)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();

            Items = new ObservableCollection<ReservationDto>();

            ItemTapped = new Command<ReservationDto>(OnItemSelected);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(UserName, StoreName));
        }
        async Task ExecuteLoadItemsCommand(string UserName, string StoreName)
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                //var items = await _reservationServices.GetReservationBySearch();

                //foreach (var item in items)
                //{
                //    Items.Add(item);
                //}

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
