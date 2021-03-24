using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public Command AddFieldCommand { get; }

        private string _storeName;

        public Command<FieldDto> ItemTapped { get; }

        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }


        public YourFieldViewModel(int storeId, string nameStore)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<FieldDto>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(storeId, nameStore));

            //UpdateFieldCommand = new Command<FieldDto>(OnUpdateField);

            AddFieldCommand = new Command(OnAddFieldAsync);

            ItemTapped = new Command<FieldDto>(OnItemSelected);
        }


        async Task ExecuteLoadItemsCommand(int storeId, string nameStore)
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = await _fieldServices.GetFieldByStoreId(storeId, 1, 20);

                StoreName = nameStore;

                foreach (var item in items)
                {
                    Items.Add(new YourFieldModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.ImageFieldModel[0].Image)))
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

        //async void OnUpdateField(FieldDto fieldDto)
        //{
        //    await Shell.Current.Navigation.PushAsync(new UpdateFieldPage(fieldDto));
        //}
        async void OnAddFieldAsync(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddFieldPage));
        }
        private async void OnItemSelected(FieldDto fieldDto)
        {
            if (fieldDto == null)
            {
                return;
            }
            await Shell.Current.Navigation.PushAsync(new UpdateFieldPage(fieldDto));
        }


        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
