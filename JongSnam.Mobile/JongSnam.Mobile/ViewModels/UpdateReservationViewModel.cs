using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Helpers;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
        private int _idPayment;
        private string _userName;
        private string _storeName;
        private string _contactMobile;
        private TimeSpan _startTime;
        private TimeSpan _stopTime;
        private string _approvalStatusString;
        private bool _isApproved;
        private bool _unApproved;
        private string _fieldName;
        private double _pricePerHour;
        private double _amount;
        private DateTime _dateBook;
        private DateTime _dateNow;
        private ImageSource _receiptPayment;
        private ImageSource _ImageProfile;
        private ValidatableObject<EnumDto> _selectedPayment;
        private string _saveTitle;
        private bool _approvalStatus;
        private string _IsFullAmountString;
        private bool _IsFullAmount;

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
        public TimeSpan StartTimes
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTimes));
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

        public DateTime DateBook
        {
            get => _dateBook;
            set
            {
                _dateBook = value;
                OnPropertyChanged(nameof(DateBook));
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
        public string IsFullAmountString
        {
            get => _IsFullAmountString;
            set
            {
                _IsFullAmountString = value;
                OnPropertyChanged(nameof(IsFullAmountString));
            }
        } 
        public bool IsFullAmount
        {
            get => _IsFullAmount;
            set
            {
                _IsFullAmount = value;
                OnPropertyChanged(nameof(IsFullAmount));
            }
        }
        public List<IsOpen> IsFulls { get; set; } = new List<IsOpen>()
        {
        new IsOpen(){Name = "จ่ายเต็มจำนวน",Value = true},
        new IsOpen(){Name = "แบ่งจ่าย",Value = false}
        };
        private IsOpen _isFull;

        public IsOpen IsFull
        {
            get
            {
                return _isFull;
            }
            set
            {
                _isFull = value;
                OnPropertyChanged(nameof(IsFull));
            }
        }


        public Command SaveCommand { get; }
        public Command UploadImageCommand { get; }

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

            Task.Run(async () => await ExecuteLoadItemsCommand(reservationId));

            SaveCommand = new Command(async () => await ExecuteSaveCommand(reservationId, approvalStatus));

            UploadImageCommand = new Command(async () =>
            {
                if (IsBusy)
                {
                    return;
                }

                var actionSheet = await Shell.Current.DisplayActionSheet("อัพโหลดรูปภาพ", "Cancel", null, "กล้อง", "แกลลอรี่");

                switch (actionSheet)
                {
                    case "Cancel":

                        // Do Something when 'Cancel' Button is pressed

                        break;

                    case "กล้อง":

                        await TakePhotoAsync();

                        break;

                    case "แกลลอรี่":

                        await PickPhotoAsync();

                        break;

                }
            });

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
                StartTimes = items.StartTime.Value.TimeOfDay;
                StopTime = items.StopTime.Value.TimeOfDay;
                IsApproved = items.ApprovalStatus.Value ? true : false;
                UnApproved = items.ApprovalStatus == false ? true : false;
                ApprovalStatusString = items.ApprovalStatus == true ? ApprovalStatusString = "อนุมัติแล้ว" : ApprovalStatusString = "ยังไม่ทำการอนุมัติ";
                FieldName = items.FieldName;
                Amount = items.AmountForPay.Value;
                PricePerHour = items.PricePerHour.Value;

                IsFullAmount = items.IsFullAmount.Value;

                IsFullAmountString = items.IsFullAmount.Value == true ? IsFullAmountString = "จ่ายเต็มจำนวน" : IsFullAmountString = "แบ่งจ่าย";



                DateBook = items.StartTime.Value;

                ReceiptPayment =
                    items.PaymentModel.Count > 0 ? ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.PaymentModel[0].Image)))
                    : null;
                ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.ImageProfile)));
                _idPayment = items.PaymentModel[0].Id.Value;
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

        async Task ExecuteSaveCommand(int reservationId, bool approvalStatus)
        {
            try
            {
                IsBusy = true;

                //var data = await _reservationServices.

                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ต้องการบันทึกการชำระเงินใช่หรือไม่ ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }

                var imageStream = await ((StreamImageSource)ReceiptPayment).Stream.Invoke(new System.Threading.CancellationToken());
                if (imageStream == null)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณาเพิ่มรูปภาพให้ถูกต้อง", "ตกลง");
                    return;
                }
                if (Amount == 0)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรอกจำนวนที่ต้องจ่ายเงิน", "ตกลง");
                    return;
                }

                var Reservation = new UpdateReservationRequest
                {
                    StartTime = new DateTime(DateBook.Year, DateBook.Month, DateBook.Day, StartTimes.Hours, StartTimes.Minutes, 0),
                    StopTime = new DateTime(DateBook.Year, DateBook.Month, DateBook.Day, StopTime.Hours, StopTime.Minutes, 0),
                };

                

                //var Paymentrequest = new PaymentRequest
                //{
                //    ReservationId = reservationId,
                //    Image = await GeneralHelper.GetBase64StringAsync(imageStream),
                //    Date = DateTime.Now,
                //    IsFullAmount = IsFull == null ? IsFullAmount : IsFull.Value,
                //    Amount = Amount
                //};
                //var result = await _paymentServices.CreatePayment(Paymentrequest);

                var resultReservation = await _reservationServices.UpdateReservation(Id ,Reservation);

                

                if (resultReservation == true)
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
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "เกิดช้อผิดพลาดบางอย่าง", "ตกลง");
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

        private async Task TakePhotoAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถใช้กล้องได้", "กล้องใช้ไม่ได้ต้องการสิทธิ์ในการเข้าถึง", "ตกลง");
                return;
            }

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถใช้กล้องได้", "แอพนี้ไม่รองรับการใช้งานกล้องของเครื่องนี้", "ตกลง");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "JongSnam",
                DefaultCamera = CameraDevice.Rear,
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                // รูปได้ค่าตอนนี้เด้อ
                ReceiptPayment = ImageSource.FromStream(() => file.GetStream());
            }
            IsBusy = false;
        }

        private async Task PickPhotoAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถเลือกรูป", "ไม่สามารถเลือกรูปได้", "ตกลง");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                ReceiptPayment = ImageSource.FromStream(() => file.GetStream());
            }
            IsBusy = false;
        }
    }
}
