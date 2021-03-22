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
    public class YourStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;
        private int _currentPage = 1;
        private int _pageSize = 5;

        public ObservableCollection<YourStoreModel> Items { get; }

        public Command LoadItemsCommand { get; }

        public Command AddStoreCommand { get; }

        public Command UpdateStoreCommand { get; }
        public Command LoadMoreCommand { get; }

        public Command<YourStore> ItemTapped { get; }

        public YourStoreViewModel()
        {

            Items = new ObservableCollection<YourStoreModel>();

            LoadMoreCommand = new Command(async () => await ExecuteLoadItemsCommand(true));

            AddStoreCommand = new Command(OnAddStore);

            UpdateStoreCommand = new Command<YourStore>(OnUpdateStore);

            ItemTapped = new Command<YourStore>(OnYourField);

            _storeServices = DependencyService.Get<IStoreServices>();

            Task.Run(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand(bool isLoadMore = false)
        {
            IsBusy = true;

            try
            {
                if (isLoadMore)
                {
                    _currentPage++;
                }
                else
                {
                    Items.Clear();
                    _currentPage = 1;
                }

                var data = await _storeServices.GetYourStores(4, _currentPage, _pageSize);
                foreach (var item in data)
                {
                    Items.Add(
                        new YourStoreModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Image)))
                        });
                }

            }
            catch (Exception ex)
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
            await Shell.Current.Navigation.PushAsync(new AddStorePage(4));
        }

        async void OnUpdateStore(YourStore item)
        {
            await Shell.Current.Navigation.PushAsync(new UpdateStorePage(item.Id.Value));
        }

        async void OnYourField(YourStore item)
        {
            if (item == null)
            {
                return;
            }
            await Shell.Current.Navigation.PushAsync(new YourFieldPage(item.Id.Value, item.Name));
        }

        public void OnAppearing()
        {
        }
    }
}
