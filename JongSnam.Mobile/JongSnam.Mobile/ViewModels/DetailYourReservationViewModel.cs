using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
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
        private System.DateTime _startTime;
        private System.DateTime _stopTime;
        private string _approvalStatus;
        private string _fieldName;
        private string _isFullAmount;
        private double _pricePerHour;
        private double _amountForPay;
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
        public System.DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }
        public System.DateTime StopTime
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
                    StartTime = (DateTime)items.StartTime;
                    StopTime = (DateTime)items.StopTime;
                    ApprovalStatus = "อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "จ่ายเต็ม";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
                    //ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.Image)));
                }
                else if(items.ApprovalStatus == false && items.IsFullAmount == false)
                {
                    Id = (int)items.Id;
                    UserName = items.UserName;
                    StoreName = items.StoreName;
                    ContactMobile = items.ContactMobile;
                    StartTime = (DateTime)items.StartTime;
                    StopTime = (DateTime)items.StopTime;
                    ApprovalStatus = "ไม่อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "แบ่งจ่าย";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
                    //ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.Image)));
                }
                else if (items.ApprovalStatus == true && items.IsFullAmount == false)
                {
                    Id = (int)items.Id;
                    UserName = items.UserName;
                    StoreName = items.StoreName;
                    ContactMobile = items.ContactMobile;
                    StartTime = (DateTime)items.StartTime;
                    StopTime = (DateTime)items.StopTime;
                    ApprovalStatus = "อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "แบ่งจ่าย";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
                    //ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(items.Image)));
                }
                else if (items.ApprovalStatus == false && items.IsFullAmount == true)
                {
                    Id = (int)items.Id;
                    UserName = items.UserName;
                    StoreName = items.StoreName;
                    ContactMobile = items.ContactMobile;
                    StartTime = (DateTime)items.StartTime;
                    StopTime = (DateTime)items.StopTime;
                    ApprovalStatus = "ไม่อนุมัติ";
                    FieldName = items.FieldName;
                    IsFullAmount = "จ่ายเต็ม";
                    PricePerHour = (double)items.PricePerHour;
                    AmountForPay = (double)items.AmountForPay;
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
