using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;
        public Command DeleteFieldCommand { get; }
        public Command SaveCommand { get; }

        private string _name;
        private string _size;
        private double _price;
        private string _isOpenString;
        private double _percentage;
        private IsOpen _privacy;
        private string _sizeField;
        private DateTime _dateNow;
        private DateTime _startDate;
        private DateTime _endDate;

        private IEnumerable<ImageFieldDto> _imageFieldDto;

        public IEnumerable<UpdateDiscountRequest> _updateDiscountRequests;
        public IEnumerable<UpdatePictureFieldRequest> _updatePictureFieldRequest;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private bool _isOpenbool;
        private string _detail;

        public string IsOpenString
        {
            get => _isOpenString;
            set
            {
                _isOpenString = value;
                OnPropertyChanged(nameof(IsOpenString));
            }
        }
        public double Percentage
        {
            get => _percentage;
            set
            {
                _percentage = value;
                OnPropertyChanged(nameof(Percentage));
            }
        }
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }
        public System.DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public System.DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public string Detail
        {
            get => _detail;
            set
            {
                _detail = value;
                OnPropertyChanged(nameof(Detail));
            }
        }


        public IEnumerable<ImageFieldDto> ImageFieldDto
        {
            get => _imageFieldDto;
            set
            {
                _imageFieldDto = value;
                OnPropertyChanged(nameof(ImageFieldDto));
            }
        }
        public IEnumerable<UpdateDiscountRequest> UpdateDiscountRequest
        {
            get => _updateDiscountRequests;
            set
            {
                _updateDiscountRequests = value;
                OnPropertyChanged(nameof(UpdateDiscountRequest));
            }
        }
        public IEnumerable<UpdatePictureFieldRequest> UpdatePictureFieldRequest
        {
            get => _updatePictureFieldRequest;
            set
            {
                _updatePictureFieldRequest = value;
                OnPropertyChanged(nameof(UpdatePictureFieldRequest));
            }
        }
        public List<IsOpen> Privacies { get; set; } = new List<IsOpen>()
        {
            new IsOpen(){Name = "เปิดบริการ",Value = true},
            new IsOpen(){Name = "ปิดบริการ",Value = false}
        };



        public IsOpen Privacy
        {
            get
            {
                return _privacy;
            }
            set
            {
                _privacy = value;
                OnPropertyChanged(nameof(Privacy));
            }
        }
        public List<IsOpen> SizeFields { get; set; } = new List<IsOpen>()
        {
            new IsOpen(){Name = "เหมาะสำหรับ 5คน"},
            new IsOpen(){Name = "เหมาะสำหรับ 7คน"},
            new IsOpen(){Name = "เหมาะสำหรับ 11คน"}
        };
        public string SizeField
        {
            get => _sizeField;
            set
            {
                _sizeField = value;
                OnPropertyChanged(nameof(SizeField));
            }
        }
        public System.DateTime DateNow
        {
            get => _dateNow;
            set
            {
                _dateNow = value;
                OnPropertyChanged(nameof(DateNow));
            }
        }


        public ImageSource ImageProfile { get; private set; }

        public UpdateFieldViewModel(FieldDto fieldDto)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            DeleteFieldCommand = new Command(async () => await OnDeleteFieldCommandAlertYesNoClicked(fieldDto.Id.Value));

            SaveCommand = new Command(async () => await OnSaveCommandAlertYesNoClicked(fieldDto.Id.Value));

            Task.Run(async () => await ExecuteLoadItemsCommand(fieldDto.Id.Value));
        }
        async Task ExecuteLoadItemsCommand(int fieldId)
        {
            IsBusy = true;
            try
            {
                DateNow = DateTime.Now;

                var data = await _fieldServices.GetFieldById(fieldId);
                Name = data.Name;
                Price = data.Price.Value;
                _isOpenbool = data.IsOpen.Value;
                if (data.IsOpen.Value)
                {
                    IsOpenString = "เปิดบริการ";
                }
                else
                {
                    IsOpenString = "ปิดบริการ";
                }
                
                ImageFieldDto = data.ImageFieldDto;
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


        public void OnAppearing()
        {
            IsBusy = true;
        }
        async Task OnDeleteFieldCommandAlertYesNoClicked(int fieldId)
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "ต้องการที่จะลบจริงๆใช่ไหม ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }
            IsBusy = true;
            var statusSaved = await _fieldServices.DeleteField(fieldId);
            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกลบเรียบร้อยแล้ว", "ตกลง");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถลบข้อมูลได้", "ตกลง");
            }
            await Shell.Current.GoToAsync("..");
        }

        async Task OnSaveCommandAlertYesNoClicked(int fieldId)
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }
            IsBusy = true;

            var request = new UpdateFieldRequest
            {
                Name = Name,
                IsOpen = Privacy == null ? _isOpenbool : Privacy.Value,
                Price = (int)Price,
                Size = Size,
                UpdateDiscountRequest = (UpdateDiscountRequest)UpdateDiscountRequest,
                UpdatePictureFieldRequest = (IList<UpdatePictureFieldRequest>)UpdatePictureFieldRequest
                //ไม่มีสถานะร้าน
            };
            var statusSaved = await _fieldServices.UpdateField(fieldId, request);

            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
            }

            await Shell.Current.GoToAsync("..");
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

            //เอาไว้เช็คว่าออกมาจากกล้องหรือยัง
            //_isBackFromChooseImage = true;

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
                ImageProfile = ImageSource.FromStream(() => file.GetStream());
            }
            //เอาไว้เช็คว่าออกมาจากกล้องหรือยัง
            //_isBackFromChooseImage = false;
        }

        private async Task PickPhotoAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถเลือกรูป", "ไม่สามารถเลือกรูปได้", "ตกลง");
                return;
            }

            //เอาไว้เช็คว่าออกมาจากคลังภาพหรือยัง
            //_isBackFromChooseImage = true;

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                ImageProfile = ImageSource.FromStream(() => file.GetStream());
            }

            //เอาไว้เช็คว่าออกมาจากคลังภาพหรือยัง
            //_isBackFromChooseImage = false;
        }
    }
}
