using System;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class FieldViewModel : BaseViewModel
    {
        private readonly IReservationServices _reservationServices;

        private readonly IFieldServices _fieldServices;
        
        private TimeSpan _fromTime;
        private TimeSpan _toTime;
        private DateTime _selectedDate = DateTime.Now;
        private string _nameField;
        private string _sizeField;
        private string _price;
        private string _isOpen;
        private string _storeName;

        public Command BookCommand { get; private set; }

        public TimeSpan FromTime
        {
            get
            {
                return _fromTime;
            }

            set
            {
                _fromTime = value;
                OnPropertyChanged(nameof(FromTime));
            }
        }

        public TimeSpan ToTime
        {
            get
            {
                return _toTime;
            }

            set
            {
                _toTime = value;
                OnPropertyChanged(nameof(ToTime));
            }
        }

        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }

            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        public string NameField
        {
            get
            {
                return _nameField;
            }

            set
            {
                _nameField = value;
                OnPropertyChanged(nameof(NameField));
            }
        }
        public string SizeField
        {
            get
            {
                return _sizeField;
            }

            set
            {
                _sizeField = value;
                OnPropertyChanged(nameof(SizeField));
            }
        }

        public string Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public string IsOpen
        {
            get
            {
                return _isOpen;
            }

            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }
        public string StoreName
        {
            get
            {
                return _storeName;
            }

            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }



    public FieldViewModel(FieldDto fieldDto, string StoreName)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();
            _fieldServices = DependencyService.Get<IFieldServices>();

            BookCommand = new Command(async () => await ExecuteBookCommand(fieldDto));

            Task.Run(async () => await ExecuteLoadItemsCommand(fieldDto, StoreName));
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadItemsCommand(FieldDto fieldDto, string storeName)
        {
            IsBusy = true;
            try
            {
                var data = await _fieldServices.GetFieldById(fieldDto.Id.Value);

                NameField = data.Name;
                Price = data.Price.Value.ToString() + " /ชม";
                IsOpen = data.IsOpen == true ? IsOpen = "เปิดบริการ" : IsOpen = "ปิดบริการ";
                SizeField = data.Size;
                StoreName = storeName;
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

        async Task ExecuteBookCommand(FieldDto fieldDto)
        {
            try
            {
                //IsBusy = true;


                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, string.Empty);
                var storeId = Preferences.Get(AuthorizeConstants.StoreIdKey, string.Empty);

                var request = new ReservationRequest
                {
                    StoreId = Convert.ToInt32(storeId),
                    UserId = Convert.ToInt32(userId),
                    FieldId = fieldDto.Id.Value,
                    StartTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, FromTime.Hours, FromTime.Minutes, 0),
                    StopTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, ToTime.Hours, ToTime.Minutes, 0),
                };

                var start = request.StartTime.Value.ToString("MM/dd/yyyy h:mm tt");
                var stop = request.StopTime.Value.ToString("MM/dd/yyyy h:mm tt");

                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", $"\n ชื่อร้านที่จอง: {StoreName} \n ชื่อสนามที่จอง: {NameField} \n ราคา : {Price} \n ขนาดของสนาม : {SizeField} \n ตั้งแต่ : {start} \n " +
                    $"ถึง : {stop}\n" , "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }
                var result = await _reservationServices.CreateReservation(request);
                if(result)
                {
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("ไม่สามารถจองได้ในขณะนี้", "กรุณารลองใหม่ภายหลัง", "ตกลง");
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
    }
}
