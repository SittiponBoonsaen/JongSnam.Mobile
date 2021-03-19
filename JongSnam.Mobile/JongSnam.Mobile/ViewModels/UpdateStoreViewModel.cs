using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;


        private readonly IAddressServices _addressServices;

        public ValidatableObject<ImageSource> BillImage { get; set; }

        public Command LoadItemsCommand { get; }
        public Command UploadImageCommand { get; private set; }

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
        private bool _isOpen;
        private string _officeHours;


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
        public string SubDistrict
        {
            get => _subDistrict;
            set
            {
                _subDistrict = value;
                OnPropertyChanged(nameof(SubDistrict));
            }
        }
        public string District
        {
            get => _district;
            set
            {
                _district = value;
                OnPropertyChanged(nameof(District));
            }
        }

       public string Province
        {
            get => _province;
            set
            {
                _province = value;
                OnPropertyChanged(nameof(Province));
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

        public UpdateStoreViewModel(int idStore)
        {
            _storeServices = DependencyService.Get<IStoreServices>();

            _addressServices = DependencyService.Get<IAddressServices>();

            Task.Run(async () => await ExecuteLoadItemsCommand(idStore)); 
            
            UploadImageCommand = new Command(() =>
            {

                Task.Run(async () => await TakePhotoAsync());
                //ShowPhotoActionSheet();
            });
        }
        async Task ExecuteLoadItemsCommand(int idStore)
        {
            IsBusy = true;
            try
            {
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
                IsOpen = (bool)dataStore.IsOpen;
                OfficeHours = dataStore.OfficeHours;

                SubDistrict = subDistrict.Name;
                District = district.Name;
                Province = province.Name;





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

        private void ShowPhotoActionSheet()
        {
            var config = new ActionSheetConfig();

            config.Options.Add(new ActionSheetOption(text: "ถ่ายรูป", icon: "camera", action: async () =>
            {
                await TakePhotoAsync();
            }));

            config.Options.Add(new ActionSheetOption(text: "คลังภาพ", icon: "image", action: async () =>
            {
                await PickPhotoAsync();
            }));

            config.SetDestructive(text: "ยกเลิก", icon: "close_box", action: () => BillImage.Validate());

            config.SetUseBottomSheet(true);

            UserDialog.ActionSheet(config);
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
                Directory = "uco",
                DefaultCamera = CameraDevice.Rear,
                PhotoSize = PhotoSize.Large,
                CompressionQuality = 70,
                MaxWidthHeight = 1024
            });

            if (file != null)
            {
                // รูปได้ค่าตอนนี้เด้อ
                BillImage.Value = ImageSource.FromStream(() => file.GetStream());

                BillImage.Validate();
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
                BillImage.Value = ImageSource.FromStream(() => file.GetStream());

                BillImage.Validate();
            }

            //เอาไว้เช็คว่าออกมาจากคลังภาพหรือยัง
            //_isBackFromChooseImage = false;
        }
    }
}
