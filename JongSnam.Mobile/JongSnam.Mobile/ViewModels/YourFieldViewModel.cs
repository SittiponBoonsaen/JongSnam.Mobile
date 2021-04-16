using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
    public class YourFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;
        public Command LoadItemsCommand { get; }
        public Command<FieldDto> BookingCommand { get; }
        public Command UpdateFieldCommand { get; }
        public ObservableCollection<FieldDto> Items { get; }

        public Command AddFieldCommand { get; }

        private string _storeName;
        private int _storeId;

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

        public string IsOpenString { get; private set; }

        public YourFieldViewModel(int storeId, string nameStore)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<FieldDto>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(storeId, nameStore));

            BookingCommand = new Command<FieldDto>(OnBookingCommand);

            AddFieldCommand = new Command(OnAddFieldAsync);

            ItemTapped = new Command<FieldDto>(OnItemSelected);
            _storeId = storeId;

            //Task.Run(async () => await ExecuteLoadItemsCommand(storeId, nameStore));
            
        }

        async void OnBookingCommand(FieldDto fieldDto)
        {
            await Shell.Current.Navigation.PushAsync(new BookingPage(fieldDto, StoreName));
        }

        async Task ExecuteLoadItemsCommand(int storeId, string nameStore)
        {
            try
            {
                IsBusy = true;
                Items.Clear();
                var data = await _fieldServices.GetFieldByStoreId(storeId, 1, 20);
                if (data == null)
                {
                    return;
                }
                StoreName = nameStore;
                foreach (var item in data)
                {
                    Items.Add(new YourFieldModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PriceString = item.Price.ToString() + "/ชั่วโมง",
                        IsOpenString = item.IsOpen == true ? IsOpenString = "เปิดบริการ" : IsOpenString = "ปิดบริการ",
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.ImageFieldModel[0].Image)))
                    });
                }
                //Count =  Items.Count().ToString();

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
            await Shell.Current.Navigation.PushAsync(new AddFieldPage(_storeId, StoreName));
        }
        private async void OnItemSelected(FieldDto fieldDto)
        {
            if (fieldDto == null)
            {
                return;
            }
            await Shell.Current.Navigation.PushAsync(new UpdateFieldPage(fieldDto, _storeId));
        }


        internal async void OnAppearingAsync()
        {
            IsBusy = true;
            var isLoggedIn = Preferences.Get(AuthorizeConstants.IsLoggedInKey, string.Empty);
            if (isLoggedIn != "True")
            {
                await Shell.Current.GoToAsync("//LoginPage");
            }
        }
    }
}
