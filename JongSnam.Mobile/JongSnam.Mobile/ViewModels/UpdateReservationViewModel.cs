using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateReservationViewModel : BaseViewModel
    {
        private readonly IReservationServices _reservationServices;
        private readonly IPaymentServices _paymentServices;

        public ObservableCollection<ReservationDto> Items { get; }
        public ObservableCollection<EnumDto> PaymentMethodList { get; set; }


        private int _id;
        private string _userName;
        private string _storeName;
        private string _contactMobile;
        private DateTime _startTime;
        private DateTime _stopTime;
        private string _approvalStatusString;
        private bool _isApproved;
        private bool _unApproved;
        private string _fieldName;
        private double _pricePerHour;
        private double _amount;
        private string _dateBook;
        private ImageSource _receiptPayment;
        private ImageSource _ImageProfile;
        private ValidatableObject<EnumDto> _selectedPayment;
        private string _saveTitle;
        private bool _approvalStatus;

        public string SaveTitle
        {
            get => _saveTitle;
            set
            {
                _saveTitle = value;
                OnPropertyChanged(nameof(SaveTitle));
            }
        }
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
        public DateTime StartTimes
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTimes));
            }
        }
        public DateTime StopTime
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

        public double PricePerHour
        {
            get => _pricePerHour;
            set
            {
                _pricePerHour = value;
                OnPropertyChanged(nameof(PricePerHour));
            }
        }


        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
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

        public string PaymentName
        {
            get => _dateBook;
            set
            {
                _dateBook = value;
                OnPropertyChanged(nameof(PaymentName));
            }
        }
        public ValidatableObject<EnumDto> SelectedPayment
        {
            get
            {
                return _selectedPayment;
            }

            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
            }
        }
        public bool ApprovalStatus
        {
            get => _approvalStatus;
            set
            {
                _approvalStatus = value;
                OnPropertyChanged(nameof(ApprovalStatus));
            }
        }

        public Command SaveCommand { get; }

        public UpdateReservationViewModel(int reservationId, bool approvalStatus)
        {
            ApprovalStatus = approvalStatus;
            var userType = Preferences.Get(AuthorizeConstants.UserTypeKey, string.Empty);
            if (userType == "Customer" && ApprovalStatus == true)
            {
                //ถ้าอนุมัติแล้วลูกค้าจะไม่สามารถแก้ไขได้
            }
            else
            {
                //แก้ไชได้
            }

            _reservationServices = DependencyService.Get<IReservationServices>();
            _paymentServices = DependencyService.Get<IPaymentServices>();

            PaymentMethodList = new ObservableCollection<EnumDto>
            {
                new EnumDto
                {
                    Id = 1,
                    Name = "จ่ายเต็มจำนวน"
                },
                new EnumDto
                {
                    Id = 2,
                    Name = "แบ่งจ่าย"
                }
            };

            Task.Run(async () => await ExecuteLoadItemsCommand(reservationId));

            SaveCommand = new Command(async () => await ExecuteSaveCommand(reservationId, approvalStatus));
        }

        async Task ExecuteSaveCommand(int reservationId,bool approvalStatus)
        {
            try
            {
                //var data = await _reservationServices.
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
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
                StartTimes = items.StartTime.Value;
                StopTime = items.StopTime.Value;
                IsApproved = items.ApprovalStatus.Value ? true : false;
                UnApproved = items.ApprovalStatus == false ? true : false;
                ApprovalStatusString = items.ApprovalStatus == true ? ApprovalStatusString = "อนุมัติแล้ว" : ApprovalStatusString = "ยังไม่ทำการอนุมัติ";
                FieldName = items.FieldName;
                Amount = items.AmountForPay.Value;
                PricePerHour = items.PricePerHour.Value;

                var paymentId = items.IsFullAmount.Value ? 1 : 2;

                SelectedPayment.Value = PaymentMethodList.Where(w => w.Id.Value == paymentId).FirstOrDefault();

                DateBook = items.StartTime.Value.Date.ToString("dd/MMMM/yyyy");
                ReceiptPayment =
                    items.PaymentModel.Count > 0 ? ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.PaymentModel[0].Image)))
                    : null;
                ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.ImageProfile)));


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

            internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
