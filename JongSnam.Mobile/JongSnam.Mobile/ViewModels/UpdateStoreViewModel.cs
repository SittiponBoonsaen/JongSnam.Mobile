using System;
using System.Collections.Generic;
using System.IO;
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
    public class UpdateStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;

        private readonly IEnumServices _enumServices;

        private readonly IAddressServices _addressServices;


        private ImageSource _imageProfile;
        public Command LoadItemsCommand { get; }
        public Command UploadImageCommand { get; private set; }
        public Command SaveCommand { get; private set; }

        private string _name;
        private string _address;
        private string _subDistrict;
        private string _district;
        private string _province;
        private string _contactMobile;
        private double _latitude;
        private double _longtitude;
        private string _rules;
        private string _image;
        private string _isOpen;
        private bool _isOpenbool;
        private string _officeHours;

        private int _distrctId;
        private int _subDistrictId;
        private int _provinceId;

        private List<EnumDto> _listprovince;
        private List<EnumDto> _listdistrict;
        private List<EnumDto> _listsubDistrict;
        private ValidatableObject<EnumDto> _selectedProvince;
        private ValidatableObject<EnumDto> _selectedDistrict;
        private ValidatableObject<EnumDto> _selectedSubDistrict;
        public Command LoadDistrictCommand { get; private set; }

        public Command LoadSubDistrictCommand { get; private set; }

        public ImageSource ImageProfile
        {
            get { return _imageProfile; }
            set
            {
                _imageProfile = value;
                OnPropertyChanged(nameof(ImageProfile));
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
        public string SubDistrictString
        {
            get => _subDistrict;
            set
            {
                _subDistrict = value;
                OnPropertyChanged(nameof(SubDistrictString));
            }
        }
        public string DistrictString
        {
            get => _district;
            set
            {
                _district = value;
                OnPropertyChanged(nameof(DistrictString));
            }
        }

        public string ProvinceString
        {
            get => _province;
            set
            {
                _province = value;
                OnPropertyChanged(nameof(ProvinceString));
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

        public string IsOpenString
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpenString));
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

        public List<EnumDto> ListSubDistrict
        {
            get => _listsubDistrict;
            set
            {
                _listsubDistrict = value;
                OnPropertyChanged(nameof(ListSubDistrict));
            }
        }
        public List<EnumDto> ListDistrict
        {
            get
            {
                return _listdistrict;
            }

            set
            {
                _listdistrict = value;
                OnPropertyChanged(nameof(ListDistrict));
            }
        }

        public List<EnumDto> ListProvince
        {
            get
            {
                return _listprovince;
            }

            set
            {
                _listprovince = value;
                OnPropertyChanged(nameof(ListProvince));
            }
        }

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




        public UpdateStoreViewModel(int idStore)
        {
            InitValidation();

            _storeServices = DependencyService.Get<IStoreServices>();

            _addressServices = DependencyService.Get<IAddressServices>();

            _enumServices = DependencyService.Get<IEnumServices>();

            Task.Run(async () => await ExecuteLoadItemsCommand(idStore));

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

            SaveCommand = new Command(async () => await ExecuteSaveCommand(idStore));

            LoadDistrictCommand = new Command(async () => await LoadDistrictEnum(SelectedProvince.Value.Id.Value));

            LoadSubDistrictCommand = new Command(async () => await LoadSubDistrictEnum(SelectedDistrict.Value.Id.Value));
        }

        private void InitValidation()
        {
            _selectedProvince = new ValidatableObject<EnumDto>();
            _selectedDistrict = new ValidatableObject<EnumDto>();
            _selectedSubDistrict = new ValidatableObject<EnumDto>();
            _selectedSubDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกตำบล" });
            _selectedDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกอำเภอ" });
            _selectedProvince.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกจังหวัด" });

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

        async Task ExecuteLoadItemsCommand(int idStore)
        {
            IsBusy = true;
            try
            {
                ListProvince = await _enumServices.GetProvinces();

                var dataStore = await _storeServices.GetStoreById(idStore);
                var subDistrict = await _addressServices.GetSubDistrictById((int)dataStore.SubDistrictId);
                var district = await _addressServices.GetDistrictById((int)dataStore.DistrictId);
                var province = await _addressServices.GetProvinceById((int)dataStore.ProvinceId);

                Name = dataStore.Name;
                Address = dataStore.Address;
                ContactMobile = dataStore.ContactMobile;
                Latitude = (double)dataStore.Latitude;
                Longtitude = (double)dataStore.Longtitude;
                Rules = dataStore.Rules;
                Image = dataStore.Image;
                _isOpenbool = dataStore.IsOpen.Value;
                if (dataStore.IsOpen.Value)
                {
                    IsOpenString = "เปิดบริการ";
                }
                else
                {
                    IsOpenString = "ปิดบริการ";
                }
                OfficeHours = dataStore.OfficeHours;

                SubDistrictString = subDistrict.Name;
                DistrictString = district.Name;
                ProvinceString = province.Name;
                if (!(String.IsNullOrEmpty(dataStore.Image)))
                {
                    ImageProfile = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(dataStore.Image)));
                }
                else
                {
                    ImageProfile = ImageSource.FromUri(new Uri("https://image.makewebeasy.net/makeweb/0/xOIgxrdh9/Document/Compac_spray_small_size_1.pdf"));
                }


                _distrctId = dataStore.DistrictId.Value;
                _subDistrictId = dataStore.SubDistrictId.Value;
                _provinceId = dataStore.ProvinceId.Value;
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

        async Task ExecuteSaveCommand(int idStore)
        {
            try
            {
                bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "ใช่", "ไม่");

                if (!answer)
                {
                    return;
                }

                IsBusy = true;

                var imageStream = await ((StreamImageSource)ImageProfile).Stream.Invoke(new System.Threading.CancellationToken());

                var request = new UpdateStoreRequest
                {
                    Image = await GeneralHelper.GetBase64StringAsync(imageStream),
                    Name = Name,
                    Address = Address,
                    SubDistrict = SelectedSubDistrict.Value == null ? _subDistrictId : SelectedSubDistrict.Value.Id.Value,//SubDistrict
                    District = SelectedDistrict.Value == null ? _distrctId : SelectedDistrict.Value.Id.Value,// District,
                    Province = SelectedProvince.Value == null ? _provinceId : SelectedProvince.Value.Id.Value,//Province,
                    ContactMobile = ContactMobile,
                    Latitude = Latitude,
                    Longtitude = Longtitude,
                    OfficeHours = OfficeHours,
                    IsOpen = Privacy == null ? _isOpenbool : Privacy.Value,
                    Rules = Rules,
                    
                };
                var statusSaved = await _storeServices.UpdateStore(idStore, request);

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


        async Task LoadDistrictEnum(int provinceId)
        {
            try
            {
                IsBusy = true;

                ListDistrict = await _enumServices.GetDistrict(provinceId);
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

                ListSubDistrict = await _enumServices.GetSubDistrict(subDistrict);
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
