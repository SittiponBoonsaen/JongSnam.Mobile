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
<<<<<<< HEAD

=======
>>>>>>> e673e83236dfb0defa44a692000c55b658177881
        public Command LoadItemsCommand { get; }
        public Command UpdateFieldCommand { get; }
        public ObservableCollection<FieldDto> Items { get; }


        public YourFieldViewModel(int storeId)
        {
<<<<<<< HEAD

=======
>>>>>>> e673e83236dfb0defa44a692000c55b658177881
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<FieldDto>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(storeId));

            UpdateFieldCommand = new Command<FieldDto>(OnUpdateField);
        }

        async Task ExecuteLoadItemsCommand(int storeId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
<<<<<<< HEAD
                var data = await _fieldServices.GetFieldByStoreId(5, 1, 3);
                foreach (var item in data)
=======

                var items = await _fieldServices.GetFieldByStoreId(storeId, 1, 20);

                foreach (var item in items)
>>>>>>> e673e83236dfb0defa44a692000c55b658177881
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
<<<<<<< HEAD
        async void OnUpdateField(FieldDto fieldDto)
=======

        async void OnUpdateField(Item Item)
>>>>>>> e673e83236dfb0defa44a692000c55b658177881
        {
            await Shell.Current.GoToAsync(nameof(UpdateFieldPage));
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
