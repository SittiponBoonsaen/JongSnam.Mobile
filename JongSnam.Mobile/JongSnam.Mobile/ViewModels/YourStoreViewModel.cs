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
    public class YourStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;

        public ObservableCollection<YourStore> Items { get; }

        public Command LoadItemsCommand { get; }

        public Command AddStoreCommand { get; }

        public Command UpdateStoreCommand { get; }

        public Command YourFieldCommand { get; }

        public YourStoreViewModel()
        {
            _storeServices = DependencyService.Get<IStoreServices>();

            Items = new ObservableCollection<YourStore>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


            AddStoreCommand = new Command(OnAddStore);

            UpdateStoreCommand = new Command<YourStore>(OnUpdateStore);

            YourFieldCommand = new Command<YourStore>(OnYourField);

        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;


            try
            {
                Items.Clear();
                var data = await _storeServices.GetYourStores(4, 1, 5);
                foreach (var item in data)
                {
                    Items.Add(item);
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnAddStore(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddStorePage));
        }

        async void OnUpdateStore(YourStore yourStore)
        {
            await Shell.Current.GoToAsync(nameof(UpdateStorePage));
        }
        async void OnYourField(YourStore yourStore)
        {

            await Shell.Current.GoToAsync(nameof(YourFieldPage));
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
