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
        private string _imageBase64;
        private Xamarin.Forms.ImageSource _image;

        public ObservableCollection<StoreDto> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
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
        public string ImageBase64
        {
            get { return _imageBase64; }
            set
            {
                _imageBase64 = value;
                OnPropertyChanged("ImageBase64");
                Image = Xamarin.Forms.ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(_imageBase64)));
            }
        }

        public Xamarin.Forms.ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }




        public ItemsViewModel()
        {
            _storeServices = DependencyService.Get<IStoreServices>();

            Title = "JongSnamFootBall";

            Items = new ObservableCollection<StoreDto>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<StoreDto>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _storeServices.GetStores(1, 10);


                foreach (var item in items)
                {
                    Items.Add(item);
                    Image = item.Image;
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

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(StoreDto storeDto)
        {
            if (storeDto == null)
            {
                return;
            }
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={storeDto.Id}");
        }
    }
}