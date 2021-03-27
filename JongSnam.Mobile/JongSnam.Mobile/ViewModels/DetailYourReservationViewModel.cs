using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class DetailYourReservationViewModel : BaseViewModel
    {
        public ObservableCollection<ReservationDto> Items { get; }

        private readonly IReservationServices _reservationServices;

        public Command CommitApprovalStatusCommand { get; }
        public Command CancelApprovalStatusCommand { get; }

        private int _id;
        private string _userName;
        private string _storeName;
        private string _contactMobile;
        private TimeSpan _startTime;
        private TimeSpan _stopTime;
        private string _approvalStatus;
        private string _fieldName;
        private string _isFullAmount;
        private double _pricePerHour;
        private double _amountForPay;
        private System.DateTime _dateTime;
        private ImageSource _ImageSource;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }
        public string ContactMobile
        {
            get => _contactMobile;
            set
            {
                _contactMobile = value;
                OnPropertyChanged(nameof(ContactMobile));
            }
        }
        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }
        public TimeSpan StopTime
        {
            get => _stopTime;
            set
            {
                _stopTime = value;
                OnPropertyChanged(nameof(StopTime));
            }
        }
        public string ApprovalStatus
        {
            get => _approvalStatus;
            set
            {
                _approvalStatus = value;
                OnPropertyChanged(nameof(ApprovalStatus));
            }
        }
        public string FieldName
        {
            get => _fieldName;
            set
            {
                _fieldName = value;
                OnPropertyChanged(nameof(FieldName));
            }
        }

        public string IsFullAmount
        {
            get => _isFullAmount;
            set
            {
                _isFullAmount = value;
                OnPropertyChanged(nameof(IsFullAmount));
            }
        }

        public double PricePerHour
        {
            get => _pricePerHour;
            set
            {
                _pricePerHour = value;
                OnPropertyChanged(nameof(PricePerHour));
            }
        }

        public double AmountForPay
        {
            get => _amountForPay;
            set
            {
                _amountForPay = value;
                OnPropertyChanged(nameof(AmountForPay));
            }
        }
        public ImageSource ImageSource
        {
            get => _ImageSource;
            set
            {
                _ImageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        public System.DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }

        public DetailYourReservationViewModel(int reservationId)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();

            Task.Run(async () => await ExecuteLoadItemsCommand(reservationId));

            CommitApprovalStatusCommand = new Command(async () => await OnCommitApprovalStatusCommand(reservationId));

            CancelApprovalStatusCommand = new Command(async () => await OnCancelApprovalStatusCommand(reservationId));
        }

        async Task ExecuteLoadItemsCommand(int reservationId)
        {
            IsBusy = true;

            try
            {
                var items = await _reservationServices.GetShowDetailYourReservation(reservationId);
                var aa = Items;
                if (items.ApprovalStatus == true && items.IsFullAmount == true)
                {
                    Id = (int)items.Id;
                    UserName = items.UserName;
                    StoreName = items.StoreName;
                    ContactMobile = items.ContactMobile;
                    StartTime = items.StartTime.Value.TimeOfDay;
                    StopTime = items.StopTime.Value.TimeOfDay;
                    ApprovalStatus = "อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "จ่ายเต็ม";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
                    DateTime = items.StartTime.Value.Date;
                    //ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.Image)));
                }
                else if (items.ApprovalStatus == false && items.IsFullAmount == false)
                {
                    Id = (int)items.Id;
                    UserName = items.UserName;
                    StoreName = items.StoreName;
                    ContactMobile = items.ContactMobile;
                    StartTime = items.StartTime.Value.TimeOfDay;
                    StopTime = items.StopTime.Value.TimeOfDay;
                    ApprovalStatus = "ไม่อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "แบ่งจ่าย";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
                    DateTime = items.StartTime.Value.Date;
                    //ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.Image)));
                }
                else if (items.ApprovalStatus == true && items.IsFullAmount == false)
                {
                    Id = (int)items.Id;
                    UserName = items.UserName;
                    StoreName = items.StoreName;
                    ContactMobile = items.ContactMobile;
                    StartTime = items.StartTime.Value.TimeOfDay;
                    StopTime = items.StopTime.Value.TimeOfDay;
                    ApprovalStatus = "อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "แบ่งจ่าย";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
                    DateTime = items.StartTime.Value.Date;
                    //ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.Image)));
                }
                else if (items.ApprovalStatus == false && items.IsFullAmount == true)
                {
                    Id = (int)items.Id;
                    UserName = items.UserName;
                    StoreName = items.StoreName;
                    ContactMobile = items.ContactMobile;
                    StartTime = items.StartTime.Value.TimeOfDay;
                    StopTime = items.StopTime.Value.TimeOfDay;
                    ApprovalStatus = "ไม่อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "จ่ายเต็ม";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
                    DateTime = items.StartTime.Value.Date;
                    //ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.Image)));
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
        async Task OnCommitApprovalStatusCommand(int reservationId)
        {
            IsBusy = true;
            try
            {
                ReservationApprovalRequest request = new ReservationApprovalRequest
                {
                    ApprovalStatus = true
                };

                var statusSaved = await _reservationServices.UpdateApprovalStatus(reservationId, request);

                if (statusSaved)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
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
        async Task OnCancelApprovalStatusCommand(int reservationId)
        {
            IsBusy = true;
            try
            {
                ReservationApprovalRequest request = new ReservationApprovalRequest
                {
                    ApprovalStatus = false
                };

                var statusSaved = await _reservationServices.UpdateApprovalStatus(reservationId, request);

                if (statusSaved)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
                    await Shell.Current.Navigation.PopAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
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
