using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ResultSearchItemViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;

        public ObservableCollection<FieldDto> Items { get; }
        public Command<FieldDto> ItemTapped { get; }
        public Command LoadItemsCommand { get; }

        public ResultSearchItemViewModel(double startPrice, double toPrice, int districtId, int provinceId)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<FieldDto>();

            ItemTapped = new Command<FieldDto>(OnItemSelected);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(startPrice, toPrice, districtId, provinceId));
        }
        async Task ExecuteLoadItemsCommand(double startPrice, double toPrice, int districtId, int provinceId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _fieldServices.GetFieldBySearch(startPrice, toPrice, districtId, provinceId, 1, 10);
                foreach (var item in items)
                {
                    Items.Add(item);
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
        private async void OnItemSelected(FieldDto fieldDto)
        {
            //await Shell.Current.Navigation.PushAsync(new DetailYourReservationPage(reservationDto.Id.Value));
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
