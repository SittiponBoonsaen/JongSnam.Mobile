using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class SearchItemViewModel : BaseViewModel
    {
        private readonly IEnumServices _enumServices;

        private List<EnumDto> _province;
        private List<EnumDto> _district;

        private ValidatableObject<EnumDto> _selectedProvince;
        private ValidatableObject<EnumDto> _selectedDistrict;

        public Command SelectedProvinceIndexChangedCommand { get; private set; }
        public Command SelectedDistrictIndexChangedCommand { get; private set; }

        public Command LoadDistrictCommand { get; private set; }

        public Command LoadSubDistrictCommand { get; private set; }

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

        public SearchItemViewModel()
        {
            _enumServices = DependencyService.Get<IEnumServices>();
            InitValidation();

            SelectedProvinceIndexChangedCommand = new Command(() => _selectedProvince.Validate());

            SelectedDistrictIndexChangedCommand = new Command(() => _selectedDistrict.Validate());

            LoadDistrictCommand = new Command(async () => await LoadDistrictEnum(SelectedProvince.Value.Id.Value));

            Task.Run(async () => await LoadProvinceEnum());

        }

        private void InitValidation()
        {
            _selectedProvince = new ValidatableObject<EnumDto>();
            _selectedDistrict = new ValidatableObject<EnumDto>();

            _selectedProvince.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกจังหวัด" });
            _selectedDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกอำเภอ" });
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

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
