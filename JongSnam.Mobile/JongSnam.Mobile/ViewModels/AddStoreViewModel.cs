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

        private readonly IAddressServices _addressServices;
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

        IEnumerable<ProvinceModel> _provinces;
        IEnumerable<DistrictModel> _districts;
        IEnumerable<SubDistrictModel> _subDistricts;

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


        public IEnumerable<ProvinceModel> ProvincesProperty
        {
            get => _provinces;
            set
            {
                _provinces = value;
                OnPropertyChanged(nameof(ProvincesProperty));
            }
        }

        public IEnumerable<DistrictModel> DistrictsProperty
        {
            get => _districts;
            set
            {
                _districts = value;
                OnPropertyChanged(nameof(DistrictsProperty));
            }
        }

        public IEnumerable<SubDistrictModel> SubDistrictProperty
        {
            get => _subDistricts;
            set
            {
                _subDistricts = value;
                OnPropertyChanged(nameof(SubDistrictProperty));
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





        public AddStoreViewModel()
        {
            _storeServices = DependencyService.Get<IStoreServices>();

            _addressServices = DependencyService.Get<IAddressServices>();

            SaveCommand = new Command(async () => await OnSaveCommand());

            Task.Run(async () => await ExecuteLoadItemsCommand());
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
        private void InitValidation()
        {
            ImageValidata = new ValidatableObject<string>();
            ImageValidata.Validations.Add(new IsNullOrEmptyRule<string> {ValidationMessage = "Image is null"});
        }
        private bool IsValid
        {
            get
            {
                return ImageValidata.Validate();
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                IsBusy = true;

                var provinces = await _addressServices.GetProvinces();
                ProvincesProperty = provinces;

                var districts = await _addressServices.GetDistrictByProvinceId(55);
                DistrictsProperty = districts;

                var subdistricts = await _addressServices.GetSubDistrictByDistrictId(152);
                SubDistrictProperty = subdistricts;

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



        async Task OnSaveCommand()
        {
            var request = new StoreRequest
            {
                OwnerId = 5,
                Image = Image,
                Name = Name,
                Address = Address,
                District = SubDistrict,
                Amphur = District,
                Province = Province,
                ContactMobile = ContactMobile,
                Latitude = Latitude,
                Longtitude = Longtitude,
                OfficeHours = OfficeHours,
                IsOpen = IsOpen,
                Rules = Rules
             };
            

            bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            if (!answer || !IsValid)
            {
                return;
            }
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
