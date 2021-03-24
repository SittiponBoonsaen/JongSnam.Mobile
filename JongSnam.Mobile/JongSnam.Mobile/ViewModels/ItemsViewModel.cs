using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;

        private StoreDto _selectedItem;

        public ObservableCollection<StoreDto> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command SearchItemCommand { get; }
        public Command<StoreDto> ItemTapped { get; }

        public StoreDto SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }



        public ItemsViewModel()
        {
            Title = "JongSnamFootBall";

            _storeServices = DependencyService.Get<IStoreServices>();

            Items = new ObservableCollection<StoreDto>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<StoreDto>(OnItemSelected);

            SearchItemCommand = new Command(OnSearchItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _storeServices.GetStores(1, 6);
                var aa = items;
                foreach (var item in items)
                {
                    Items.Add(item);
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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async void OnSearchItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(SearchItemPage));
        }

        async void OnItemSelected(StoreDto storeDto)
        {
            if (storeDto == null)
            {
                return;
            }
            await Shell.Current.Navigation.PushAsync(new ListFieldPage(storeDto));
        }
    }
}