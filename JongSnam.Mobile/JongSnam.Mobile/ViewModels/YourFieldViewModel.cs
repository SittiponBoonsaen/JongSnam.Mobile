using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;
        public Command LoadItemsCommand { get; }
        public Command UpdateFieldCommand { get; }
        public ObservableCollection<FieldDto> Items { get; }


        public YourFieldViewModel(int storeId)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<FieldDto>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(storeId));

            UpdateFieldCommand = new Command<Item>(OnUpdateField);
        }

        async Task ExecuteLoadItemsCommand(int storeId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await _fieldServices.GetFieldByStoreId(storeId, 1, 20);

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

        async void OnUpdateField(Item Item)
        {
            await Shell.Current.GoToAsync(nameof(UpdateFieldPage));
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
