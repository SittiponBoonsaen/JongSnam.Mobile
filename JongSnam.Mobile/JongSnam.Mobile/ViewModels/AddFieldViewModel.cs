using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Helpers;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class AddFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;

        public Command SaveCommand { get; }

        private ImageSource _imageProfile;

        public Command UploadImageCommand { get; private set; }

        public ValidatableObject<string> ImageValidata { get; set; }

        private string _nameField;
        private int _priceField;
                private IsOpen _sizeField;
        private bool _isOpen;
        private double _percentage;
        private System.DateTime _startDate = DateTime.Now;
        private System.DateTime _endDate = DateTime.Now;
        private System.DateTime _dateNow;
        private string _detail;
        private string _storeName;

        public string NameField
        {
            get => _nameField;
            set
            {
                _nameField = value;
                OnPropertyChanged(nameof(NameField));
            }
        }
        public int PriceField
        {
            get => _priceField;
            set
            {
                _priceField = value;
                OnPropertyChanged(nameof(PriceField));
            }
        }
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
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
        public System.DateTime DateNow
        {
            get => _dateNow;
            set
            {
                _dateNow = value;
                OnPropertyChanged(nameof(DateNow));
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

        public ImageSource ImageProfile
        {
            get { return _imageProfile; }
            set
            {
                _imageProfile = value;
                OnPropertyChanged(nameof(ImageProfile));
            }
        }
        public List<IsOpen> SizeFields { get; set; } = new List<IsOpen>()
        {
        new IsOpen(){Name = "ขนาด 5 คน"},
        new IsOpen(){Name = "ขนาด 7 คน"},
        new IsOpen(){Name = "ขนาด 11 คน"}
        };


        public IsOpen SizeField
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

        public AddFieldViewModel(int storeId, string storeName)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Title = storeName;

            InitValidation();

            SaveCommand = new Command(async () => await OnSaveCommand(storeId));

            InitPage();

            UploadImageCommand = new Command(async () =>
            {


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

            IsBusy = false;
        }
        internal void OnAppearing()
        {
            IsBusy = true;
        }

        void InitPage()
        {
            DateNow = DateTime.Now;
            ImageProfile = ImageSource.FromUri(new Uri("https://image.makewebeasy.net/makeweb/0/xOIgxrdh9/Document/Compac_spray_small_size_1.pdf"));
            IsBusy = false;
        }

        async Task OnSaveCommand(int storeId)
        {
            try
            {
                IsBusy = true;
                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน?", "ต้องการบันทึกใช่ไหม ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }
                var imageStream = await ((StreamImageSource)ImageProfile).Stream.Invoke(new System.Threading.CancellationToken());

                if (imageStream == null)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณาเพิ่มรูปภาพ", "ตกลง");
                    return;
                }
                if (string.IsNullOrWhiteSpace(NameField))
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลชือสนาม", "ตกลง");
                    return;
                }
                if (PriceField == 0)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลราคาสนาม", "ตกลง");
                    return;
                }
                if (string.IsNullOrWhiteSpace(SizeField.Name))
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลขนาดสนาม", "ตกลง");
                    return;
                }
                var fieldRequest = new FieldRequest()
                {
                    Active = true,
                    IsOpen = true,
                    Name = NameField,
                    Price = PriceField,
                    Size = SizeField.Name,
                    StoreId = storeId
                };
                var discountRequest = new DiscountRequest()
                {
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Detail = Detail,
                    Percentage = Percentage
                };
                var imageFieldRequest = new ImageFieldRequest()
                {
                    Image = await GeneralHelper.GetBase64StringAsync(imageStream)
                };
                var request = new AddFieldRequest
                {
                    DiscountRequest = discountRequest,
                    FieldRequest = fieldRequest,
                    PictureFieldRequest = new List<ImageFieldRequest>
                {
                    imageFieldRequest
                }
                };

                var statusSaved = await _fieldServices.AddField(request);
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
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลให้ครบถ้วน", "ตกลง");
                return;
                throw ex;

            }
            finally
            {
                IsBusy = false;
            }
           
        }

        private void InitValidation()
        {
            ImageValidata = new ValidatableObject<string>();
            ImageValidata.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Image is null" });
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
