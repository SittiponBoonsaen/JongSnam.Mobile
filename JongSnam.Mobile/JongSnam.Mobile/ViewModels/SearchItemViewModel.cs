using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class SearchItemViewModel : BaseViewModel
    {
        private readonly IEnumServices _enumServices;

        private readonly IFieldServices _fieldServices;
        public ObservableCollection<FieldDto> Items { get; }

        private List<EnumDto> _province;
        private List<EnumDto> _district;

        private double _startPrice;
        private double _toPrice;

        private ValidatableObject<EnumDto> _selectedProvince;
        private ValidatableObject<EnumDto> _selectedDistrict;

        public Command SelectedProvinceIndexChangedCommand { get; private set; }
        public Command SelectedDistrictIndexChangedCommand { get; private set; }

        public Command LoadDistrictCommand { get; private set; }

        public Command LoadSubDistrictCommand { get; private set; }

        public Command SearchItemCommand { get; }

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
        public double StartPrice
        {
            get
            {
                return _startPrice;
            }

            set
            {
                _startPrice = value;
                OnPropertyChanged(nameof(StartPrice));
            }
        }
        public double ToPrice
        {
            get
            {
                return _toPrice;
            }

            set
            {
                _toPrice = value;
                OnPropertyChanged(nameof(ToPrice));
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
            _fieldServices = DependencyService.Get<IFieldServices>();

            InitValidation();

            Items = new ObservableCollection<FieldDto>();

            SelectedProvinceIndexChangedCommand = new Command(() => _selectedProvince.Validate());

            SelectedDistrictIndexChangedCommand = new Command(() => _selectedDistrict.Validate());

            LoadDistrictCommand = new Command(async () => await LoadDistrictEnum(SelectedProvince.Value.Id.Value));


            SearchItemCommand = new Command(async () => await OnSearchItemCommand(StartPrice, ToPrice, SelectedDistrict.Value.Id.Value, SelectedProvince.Value.Id.Value));

            Task.Run(async () => await ExecuteLoadItemsCommand());

        }
        public void checkvalue()
        {

        }

        private void InitValidation()
        {
            _selectedProvince = new ValidatableObject<EnumDto>();
            _selectedDistrict = new ValidatableObject<EnumDto>();

            _selectedProvince.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกจังหวัด" });
            _selectedDistrict.Validations.Add(new IsSelectedItemRule<EnumDto>() { ValidationMessage = "กรุณาเลือกอำเภอ" });
        }

        async Task ExecuteLoadItemsCommand()
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
        async Task OnSearchItemCommand(double startPrice, double toPrice, int districtId, int provinceId)
        {

            await Shell.Current.Navigation.PushAsync(new ResultSearchItemPage(startPrice, toPrice, districtId, provinceId));
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
