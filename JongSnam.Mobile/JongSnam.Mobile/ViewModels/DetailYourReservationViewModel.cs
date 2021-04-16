using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Helpers;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class DetailYourReservationViewModel : BaseViewModel
    {
        public ObservableCollection<ReservationDto> Items { get; }
        public ObservableCollection<EnumDto> PaymentMethodList { get; set; }

        private readonly IReservationServices _reservationServices;
        private readonly IPaymentServices _paymentServices;

        public Command CommitApprovalStatusCommand { get; }
        public Command CancelApprovalStatusCommand { get; }
        public Command UploadReceiptCommand { get; }
        public Command SaveCommand { get; }
        public Command EditDetailYourReservationCommand { get; }
        public Command SelectedPaymentIndexChangedCommand { get; private set; }

        private int _id;
        private string _userName;
        private string _storeName;
        private string _contactMobile;
        private string _startTime;
        private string _stopTime;
        private string _approvalStatusString;
        private bool _approvalStatus;
        private bool _isApproved;
        private bool _unApproved;
        private string _fieldName;
        private double _pricePerHour;
        private double _amount;
        private string _dateBook;
        private ImageSource _receiptPayment;
        private ImageSource _ImageProfile;
        private string _paymentName;
        private ValidatableObject<EnumDto> _selectedPayment;
        private string _saveTitle;

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

        public DetailYourReservationViewModel(int reservationId)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();
            _paymentServices = DependencyService.Get<IPaymentServices>();


            _selectedPayment = new ValidatableObject<EnumDto>();
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

            CommitApprovalStatusCommand = new Command(async () => await OnCommitApprovalStatusCommand(reservationId));

            CancelApprovalStatusCommand = new Command(async () => await OnCancelApprovalStatusCommand(reservationId));

            UploadReceiptCommand = new Command(async () => await ExecuteUploadReceiptCommandCommand());

            SaveCommand = new Command(async () => await ExecuteSaveCommandCommand(reservationId));

            SelectedPaymentIndexChangedCommand = new Command(() => _selectedPayment.Validate());

            EditDetailYourReservationCommand = new Command(async () => await OnEditDetailYourReservation(reservationId));
        }

        async Task OnEditDetailYourReservation(int reservationId)
        {
            await Shell.Current.Navigation.PushAsync(new UpdateReservationPage(reservationId, _approvalStatus));
        }

        async Task ExecuteLoadItemsCommand(int reservationId)
        {
            IsBusy = true;

            try
            {
                var userType = Preferences.Get(AuthorizeConstants.UserTypeKey, string.Empty);
                if (userType == "Owner")
                {
                    IsOwner = true;
                    IsCustomer = false;
                    SaveTitle = "";
                }
                else
                {
                    IsOwner = false;
                    IsCustomer = true;
                    SaveTitle = "บันทึก";
                }

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
                _approvalStatus = items.ApprovalStatus.Value;
                var paymentId = items.IsFullAmount.Value ? 1 : 2;
                SelectedPayment.Value = PaymentMethodList.Where(w => w.Id.Value == paymentId).FirstOrDefault();
                Amount = items.AmountForPay.Value;
                PricePerHour = items.PricePerHour.Value;

                DateBook = items.StartTime.Value.Date.ToString("dd/MMMM/yyyy");
                ReceiptPayment =
                    items.PaymentModel.Count > 0 ? ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.PaymentModel[0].Image)))
                    : null;
                ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.ImageProfile)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลให้ครบถ้วน", "ตกลง");
                return;
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
                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ต้องการอนุมัติรายการนี้ใช่หรือไม่ ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }

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

                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่ต้องการอนุมัติรายการนี้ใช่หรือไม่ ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }

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

        async Task ExecuteUploadReceiptCommandCommand()
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
        }

        async Task ExecuteSaveCommandCommand(int reservationId)
        {
            try
            {
                if (IsOwner)
                {
                    return;
                }

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

                var request = new PaymentRequest
                {
                    Image = await GeneralHelper.GetBase64StringAsync(imageStream),
                    Date = DateTime.Now,
                    ReservationId = reservationId,
                    IsFullAmount = SelectedPayment.Value.Id == 1 ? true : false,
                    Amount = Amount
                };

                var result = await _paymentServices.CreatePayment(request);

                if (result)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน", "บันทึข้อมูลเรียบร้อยแล้ว", "ตกลง");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน", "ไม่สามารถบันทึข้อมูลได้", "ตกลง");
                }

            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณาทำรายการให้ถูกต้อง", "ตกลง");
                return;
                throw ex;
            }
            finally
            {

            }
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
