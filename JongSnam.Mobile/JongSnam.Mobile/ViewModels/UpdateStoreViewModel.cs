using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;


        private readonly IAddressServices _addressServices;



        public Command LoadItemsCommand { get; }

        private string _name;
        private string _address;
        private int _subDistrictId;
        private int _districtId;
        private int _provinceId;
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
        public int SubDistrictId
        {
            get => _subDistrictId;
            set
            {
                _subDistrictId = value;
                OnPropertyChanged(nameof(SubDistrictId));
            }
        }
        public int DistrictId
        {
            get => _districtId;
            set
            {
                _districtId = value;
                OnPropertyChanged(nameof(DistrictId));
            }
        }

       public int ProvinceId
        {
            get => _provinceId;
            set
            {
                _provinceId = value;
                OnPropertyChanged(nameof(ProvinceId));
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
        }
        async Task ExecuteLoadItemsCommand(int idStore)
        {
            IsBusy = true;
            try
            {
                var dataStore = await _storeServices.GetStoreById(idStore);
                Name = dataStore.Name;
                Address = dataStore.Address;
                SubDistrictId = (int)dataStore.SubDistrictId;
                DistrictId = (int)dataStore.DistrictId;
                ProvinceId = (int)dataStore.ProvinceId;
                ContactMobile = dataStore.ContactMobile;
                Latitude = (double)dataStore.Latitude;
                Longtitude = (double)dataStore.Longtitude;
                Rules = dataStore.Rules;
                Image = dataStore.Image;
                IsOpen = (bool)dataStore.IsOpen;
                OfficeHours = dataStore.OfficeHours;


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
    }
}
