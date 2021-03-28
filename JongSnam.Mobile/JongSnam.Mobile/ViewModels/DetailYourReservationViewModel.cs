using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
        private string _startTime;
        private string _stopTime;
        private string _approvalStatusString;
        private bool _isApproved;
        private bool _unApproved;
        private string _fieldName;
        private string _isFullAmount;
        private double _pricePerHour;
        private double _amountForPay;
        private string _dateBook;
        private ImageSource _receiptPayment;
        private ImageSource _ImageProfile;

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
        public string StartTimes
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTimes));
            }
        }
        public string StopTime
        {
            get => _stopTime;
            set
            {
                _stopTime = value;
                OnPropertyChanged(nameof(StopTime));
            }
        }
        public bool IsApproved
        {
            get => _isApproved;
            set
            {
                _isApproved = value;
                OnPropertyChanged(nameof(IsApproved));
            }
        }
        public bool UnApproved
        {
            get => _unApproved;
            set
            {
                _unApproved = value;
                OnPropertyChanged(nameof(UnApproved));
            }
        }
        public string ApprovalStatusString
        {
            get => _approvalStatusString;
            set
            {
                _approvalStatusString = value;
                OnPropertyChanged(nameof(ApprovalStatusString));
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
        public ImageSource ReceiptPayment
        {
            get => _receiptPayment;
            set
            {
                _receiptPayment = value;
                OnPropertyChanged(nameof(ReceiptPayment));
            }
        }

        public ImageSource ImageProfile
        {
            get => _ImageProfile;
            set
            {
                _ImageProfile = value;
                OnPropertyChanged(nameof(ImageProfile));
            }
        }

        public string DateBook
        {
            get => _dateBook;
            set
            {
                _dateBook = value;
                OnPropertyChanged(nameof(DateBook));
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

                Id = items.Id.Value;
                UserName = items.UserName;
                StoreName = items.StoreName;
                ContactMobile = items.ContactMobile;
                StartTimes = items.StartTime.Value.TimeOfDay.ToString();
                StopTime = items.StopTime.Value.TimeOfDay.ToString();
                IsApproved = items.ApprovalStatus.Value ? true : false;
                UnApproved = items.ApprovalStatus == false ? true : false;
                ApprovalStatusString = items.ApprovalStatus == true ? ApprovalStatusString = "อนุมัติแล้ว" : ApprovalStatusString = "ยังไม่ทำการอนุมัติ";
                FieldName = items.FieldName;
                IsFullAmount = items.IsFullAmount == true ? IsFullAmount = "จ่ายเต็มจำนวน" : IsFullAmount = "แบ่งจ่าย";
                PricePerHour = items.PricePerHour.Value;
                AmountForPay = items.AmountForPay.Value;
                DateBook = items.StartTime.Value.Date.ToLongDateString();
                ReceiptPayment = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.PaymentModel[0].Image)));
                ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.ImageProfile)));
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
