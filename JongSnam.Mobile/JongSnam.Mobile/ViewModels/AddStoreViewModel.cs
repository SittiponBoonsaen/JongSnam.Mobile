using JongSnam.Mobile.Helpers;
using JongSnam.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnamService.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using JongSnam.Mobile.Constants;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using Location = Xamarin.Essentials.Location;
using Map = Xamarin.Forms.GoogleMaps.Map;

namespace JongSnam.Mobile.ViewModels
{
    public class AddStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;

        private readonly IEnumServices _enumServices;
        public Command SaveCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command UploadImageCommand { get; private set; }

        public ValidatableObject<string> ImageValidata { get; set; }
        public ValidatableObject<string> NameValidata { get; set; }
        public ValidatableObject<string> AddressValidata { get; set; }
        public ValidatableObject<string> SubDistrictValidata { get; set; }
        public ValidatableObject<string> DistrictValidata { get; set; }
        public ValidatableObject<string> ProvinceValidata { get; set; }
        public ValidatableObject<string> ContactMobileValidata { get; set; }
        public ValidatableObject<string> LatValidata { get; set; }
        public ValidatableObject<string> LongValidata { get; set; }
        public ValidatableObject<string> OfficeHoursValidata { get; set; }
        public ValidatableObject<string> RulesValidata { get; set; }
        public Command SelectedProvinceIndexChangedCommand { get; private set; }

        public Command LoadDistrictCommand { get; private set; }

        public Command LoadSubDistrictCommand { get; private set; }

        private ValidatableObject<EnumDto> _selectedProvince;
        private ValidatableObject<EnumDto> _selectedDistrict;
        private ValidatableObject<EnumDto> _selectedSubDistrict;
        private List<EnumDto> _province;
        private List<EnumDto> _district;
        private List<EnumDto> _subDistrict;

        private string _name;
        private string _address;
        private string _contactMobile;
        private double _latitude;
        private double _longtitude;
        private string _rules;
        private string _image;
        private bool _isOpen;
        private string _officeHours;
        private ImageSource _imageProfile;
        private Map _map;

        public ValidatableObject<EnumDto> SelectedProvince
        {
            get
            {
                return _selectedProvince;
            }

            set
            {
                _selectedProvince = value;
                OnPropertyChanged(nameof(SelectedProvince));
            }
        }
        public ValidatableObject<EnumDto> SelectedDistrict
        {
            get
            {
                return _selectedDistrict;
            }

            set
            {
                _selectedDistrict = value;
                OnPropertyChanged(nameof(SelectedDistrict));
            }
        }
        public ValidatableObject<EnumDto> SelectedSubDistrict
        {
            get
            {
                return _selectedSubDistrict;
            }

            set
            {
                _selectedSubDistrict = value;
                OnPropertyChanged(nameof(SelectedSubDistrict));
            }
        }
        public List<EnumDto> SubDistrict
        {
            get => _subDistrict;
            set
            {
                _subDistrict = value;
                OnPropertyChanged(nameof(SubDistrict));
            }
        }
        public List<EnumDto> District
        {
            get
            {
                return _district;
            }

            set
            {
                _district = value;
                OnPropertyChanged(nameof(District));
            }
        }

        public List<EnumDto> Province
        {
            get
            {
                return _province;
            }

            set
            {
                _province = value;
                OnPropertyChanged(nameof(Province));
            }
        }


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
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

        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }

        public double Longtitude
        {
            get => _longtitude;
            set
            {
                _longtitude = value;
                OnPropertyChanged(nameof(Longtitude));
            }
        }

        public string Rules
        {
            get => _rules;
            set
            {
                _rules = value;
                OnPropertyChanged(nameof(Rules));
            }
        }
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
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

        public string OfficeHours
        {
            get => _officeHours;
            set
            {
                _officeHours = value;
                OnPropertyChanged(nameof(OfficeHours));
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
        public List<IsOpen> Privacies { get; set; } = new List<IsOpen>()
        {
        new IsOpen(){Name = "เปิดบริการ",Value = true},
        new IsOpen(){Name = "ปิดบริการ",Value = false}
        };

        private IsOpen _privacy;
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

        public AddStoreViewModel(int userId, Map map)
        {
            _storeServices = DependencyService.Get<IStoreServices>();

            _enumServices = DependencyService.Get<IEnumServices>();

            _map = map;

            InitValidation();

            SaveCommand = new Command(async () => await OnSaveCommand(userId));

            SelectedProvinceIndexChangedCommand = new Command(() => _selectedProvince.Validate());

            LoadDistrictCommand = new Command(async () => await LoadDistrictEnum(SelectedProvince.Value.Id.Value));

            LoadSubDistrictCommand = new Command(async () => await LoadSubDistrictEnum(SelectedDistrict.Value.Id.Value));

            Task.Run(async () => await LoadProvinceEnum());

            Task.Run(async () => await InitMapLocation());

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

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private void InitValidation()
        {
            ImageValidata = new ValidatableObject<string>();
            ImageValidata.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Image is null" });
            _selectedSubDistrict = new ValidatableObject<EnumDto>();
            _selectedSubDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกตำบล" });

            _selectedDistrict = new ValidatableObject<EnumDto>();
            _selectedDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกอำเภอ" });

            _selectedProvince = new ValidatableObject<EnumDto>();
            _selectedProvince.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = MessageConstants.PleaseSelectProvince });
        }

        private bool IsValid()
        {
            return _selectedProvince.Validate();
        }

        async Task LoadProvinceEnum()
        {

            try
            {
                IsBusy = true;

                Province = await _enumServices.GetProvinces();
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

        async Task LoadDistrictEnum(int provinceId)
        {
            try
            {
                IsBusy = true;

                District = await _enumServices.GetDistrict(provinceId);
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

        async Task LoadSubDistrictEnum(int subDistrict)
        {
            try
            {
                IsBusy = true;

                SubDistrict = await _enumServices.GetSubDistrict(subDistrict);
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

        async Task OnSaveCommand(int userId)
        {
            try
            {
                if (!IsValid())
                {
                    return;
                }

                bool answer = await Shell.Current.DisplayAlert("ยืนยันข้อมูล", "ต้องการเพิ่มร้านใช่หรือไม่ ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }
                var imageStream = await ((StreamImageSource)ImageProfile).Stream.Invoke(new System.Threading.CancellationToken());

                if (imageStream == null || string.IsNullOrWhiteSpace(Name) || SelectedDistrict.Value == null || SelectedProvince.Value == null || SelectedSubDistrict.Value == null || string.IsNullOrWhiteSpace(ContactMobile) || string.IsNullOrWhiteSpace(Address) || string.IsNullOrWhiteSpace(OfficeHours))
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลให้ครบถ้วน", "ตกลง");
                    return;
                }

                if (ContactMobile.Length < 10)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "กรุณากรอกให้ครบ10หลัก", "ตกลง");
                    return;
                }

                var request = new StoreRequest
                {
                    OwnerId = userId,
                    Image = await GeneralHelper.GetBase64StringAsync(imageStream),
                    Name = Name,
                    Address = Address,
                    SubDistrictId = SelectedSubDistrict.Value.Id.Value,
                    DistrictId = SelectedDistrict.Value.Id.Value,
                    ProvinceId = SelectedProvince.Value.Id.Value,
                    ContactMobile = ContactMobile,
                    Latitude = Latitude,
                    Longtitude = Longtitude,
                    OfficeHours = OfficeHours,
                    IsOpen = Privacy == null ? false : Privacy.Value,
                    Rules = Rules
                };

                IsBusy = true;
                var statusSaved = await _storeServices.AddStore(request);
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

        async Task InitMapLocation()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(10)
                });
            }

            Pin pin = new Pin()
            {
                Type = PinType.Place,
                Label = "กดค้างแล้วลากเพื่อย้ายตำแหน่ง.",
                Position = new Position(location.Latitude, location.Longitude),
                Rotation = 33.3f,
                IsDraggable = true
            };

            _map.Pins.Add(pin);
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMeters(5000)));

            _map.PinDragEnd += (_, e) => SetLocation(e.Pin);
        }

        void SetLocation(Pin pin)
        {
            Latitude = pin.Position.Latitude;
            Longtitude = pin.Position.Longitude;
        }
    }
}
