using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ListFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;

        private string _storeName;
        private double _rating;
        private string _officeHours;
        private FieldDto _selectedItem;
        private ImageSource _imageStore;

        public Command LoadFieldCommand { get; }
        public Command<FieldDto> ItemTapped { get; }
        public Command ReviewCommand { get; }

        public ObservableCollection<ListFieldModel> Items { get; }

        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }

        public double Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public string OfficeHours
        {
            get => _officeHours;
            set
            {
                _officeHours = value;
                OnPropertyChanged(nameof(OfficeHours));
            }
        }

        public FieldDto SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public ImageSource ImageStore
        {
            get { return _imageStore; }
            set
            {
                _imageStore = value;
                OnPropertyChanged(nameof(ImageStore));
            }
        }

        public Command LoadItemsCommand { get; }

        public ListFieldViewModel(StoreDtoModel storeDto)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<ListFieldModel>();

            //LoadFieldCommand = new Command(async () => await ExecuteLoadFieldCommand(storeDto));

            Preferences.Set(AuthorizeConstants.StoreIdKey, storeDto.Id.ToString());

            //write storeId to storage
            ItemTapped = new Command<FieldDto>(OnItemSelected);

            ReviewCommand = new Command(async () => await OnReview(storeDto.Id.Value));

            Task.Run(async () => await ExecuteLoadFieldCommand(storeDto));
        }

        async Task ExecuteLoadFieldCommand(StoreDtoModel storeDto)
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                StoreName = storeDto.Name;
                OfficeHours = storeDto.OfficeHours;
                ImageStore = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(storeDto.Image)));

                var items = await _fieldServices.GetFieldByStoreId(storeDto.Id.Value, 1, 20);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Items.Add(new ListFieldModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Price = item.Price,
                            ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.ImageFieldModel[0].Image)))
                        });
                    }
                }
                    await Shell.Current.DisplayAlert("แจ้งเตือน", "บันทึข้อมูลเรียบร้อยแล้ว", "ตกลง");
                    await Shell.Current.GoToAsync("..");

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
        async void OnItemSelected(FieldDto fieldDto)
        {
            if (fieldDto == null)
            {
                return;
            }
            await Shell.Current.Navigation.PushAsync(new FieldPage(fieldDto, StoreName));
        }

        async Task OnReview(int storeId)
        {
            await Shell.Current.Navigation.PushAsync(new ReviewPage(storeId));
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
