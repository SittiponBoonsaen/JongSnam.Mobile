using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnamService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class AddStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;

        private readonly IEnumServices _enumServices;
        public Command SaveCommand { get; }
        public Command LoadItemsCommand { get; }

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

        public AddStoreViewModel(int userId)
        {
            _storeServices = DependencyService.Get<IStoreServices>();

            _enumServices = DependencyService.Get<IEnumServices>();

            InitValidation();

            SaveCommand = new Command(async () => await OnSaveCommand(userId));

            SelectedProvinceIndexChangedCommand = new Command(() => _selectedProvince.Validate());

            LoadDistrictCommand = new Command(async () => await LoadDistrictEnum(SelectedProvince.Value.Id.Value));

            LoadSubDistrictCommand = new Command(async () => await LoadSubDistrictEnum(SelectedDistrict.Value.Id.Value));

            Task.Run(async () => await LoadProvinceEnum());

        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private void InitValidation()
        {
            ImageValidata = new ValidatableObject<string>();
            ImageValidata.Validations.Add(new IsNullOrEmptyRule<string> { ValidationMessage = "Image is null" });
            _selectedProvince = new ValidatableObject<EnumDto>();
            _selectedDistrict = new ValidatableObject<EnumDto>();
            _selectedSubDistrict = new ValidatableObject<EnumDto>();
            _selectedSubDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกตำบล" });
            _selectedDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกอำเภอ" });
            _selectedProvince.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกจังหวัด" });
        }
        private bool IsValid
        {
            get
            {
                return ImageValidata.Validate();
            }
        }

        async Task LoadProvinceEnum()
        {
            try
            {
                IsBusy = true;

                Province = await _enumServices.GetProvinces();
            }
            catch(Exception ex)
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
            bool answer = await Shell.Current.DisplayAlert("Question?", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "Yes", "No");
            if (!answer)
            {
                return;
            }

            var request = new StoreRequest
            {
                OwnerId = userId,
                Image = Image,
                Name = Name,
                Address = Address,
                SubDistrictId = SelectedSubDistrict.Value.Id.Value,
                DistrictId = SelectedDistrict.Value.Id.Value,
                ProvinceId = SelectedDistrict.Value.Id.Value,
                ContactMobile = ContactMobile,
                Latitude = Latitude,
                Longtitude = Longtitude,
                OfficeHours = OfficeHours,
                IsOpen = IsOpen,
                Rules = Rules
            };

            var statusSaved = await _storeServices.AddStore(request);
            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
            }
            await Shell.Current.GoToAsync("..");
        }

    }
}
