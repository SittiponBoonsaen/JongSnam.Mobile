using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                _selectedProvince = value ;
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

            

            Items = new ObservableCollection<FieldDto>();

            SelectedProvinceIndexChangedCommand = new Command(() => _selectedProvince.Validate());

            SelectedDistrictIndexChangedCommand = new Command(() => _selectedDistrict.Validate());

            LoadDistrictCommand = new Command(async () => await LoadDistrictEnum(SelectedProvince.Value.Id.Value));


            SearchItemCommand = new Command(async () => await OnSearchItemCommand());

            Task.Run(async () => await ExecuteLoadItemsCommand());

            InitValidation();

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
        async Task OnSearchItemCommand()
        {

            double toPrice = ToPrice == 0 ? 10000 : ToPrice;

            int? pro = SelectedProvince.Value == null ? 0 : SelectedProvince.Value.Id.Value;
            if (pro == 0)
            {
                pro = null;
            }

            int? dis = SelectedDistrict.Value == null ? 0 : SelectedDistrict.Value.Id.Value;
            if (dis == 0)
            {
                dis = null;
            }


            await Shell.Current.Navigation.PushAsync(new ResultSearchItemPage(StartPrice, toPrice, dis, pro));
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
