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
        
        private TimeSpan _fromTime;
        private TimeSpan _toTime;
        private DateTime _selectedDate = DateTime.Now;
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

        public FieldViewModel(FieldDto fieldDto)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();

            BookCommand = new Command(async () => await ExecuteBookCommand(fieldDto));

        }

        internal void OnAppearing()
        {
            IsBusy = true;
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

                var result = await _reservationServices.CreateReservation(request);

                if (result)
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
