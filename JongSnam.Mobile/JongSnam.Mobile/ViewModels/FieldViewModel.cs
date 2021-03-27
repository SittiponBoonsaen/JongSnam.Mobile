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

        private double _fromTime;
        private double _toTime;
        private DateTime _selectedDate = DateTime.Now;

        public Command BookCommand { get; private set; }

        public double FromTime
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

        public double ToTime
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

            BookCommand = new Command(async () => await ExecuteBookCommand());

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteBookCommand()
        {
            try
            {
                IsBusy = true;
                var startHour = Math.Truncate(FromTime);
                var stopHour = Math.Truncate(ToTime);
                var startTime = ToTime - Math.Truncate(FromTime);
                var stopTime = ToTime - Math.Truncate(ToTime);
                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, string.Empty);

                var request = new ReservationRequest
                {
                    StoreId = 1,
                    UserId = Convert.ToInt32(userId),
                    FieldId = 1,
                    StartTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, (int)startHour, (int)startTime, 0),
                    StopTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, (int)stopHour, (int)stopTime, 0),
                };

                var aa = await _reservationServices.CreateReservation(request);
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
