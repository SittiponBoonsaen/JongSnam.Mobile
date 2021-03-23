using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamServices.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;
        public Command DeleteFieldCommand { get; }
        public Command SaveCommand { get; }

        private string _name;
        private string _size;
        private double _price;
        private bool _isOpen;
        private double _percentage;
        private IEnumerable<ImageFieldDto> _imageFieldDto;

        public IEnumerable<UpdateDiscountRequest> _updateDiscountRequests;
        public IEnumerable<UpdatePictureFieldRequest> _updatePictureFieldRequest;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
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
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        public IEnumerable<ImageFieldDto> ImageFieldDto
        {
            get => _imageFieldDto;
            set
            {
                _imageFieldDto = value;
                OnPropertyChanged(nameof(ImageFieldDto));
            }
        }
        public IEnumerable<UpdateDiscountRequest> UpdateDiscountRequest
        {
            get => _updateDiscountRequests;
            set
            {
                _updateDiscountRequests = value;
                OnPropertyChanged(nameof(UpdateDiscountRequest));
            }
        }
        public IEnumerable<UpdatePictureFieldRequest> UpdatePictureFieldRequest
        {
            get => _updatePictureFieldRequest;
            set
            {
                _updatePictureFieldRequest = value;
                OnPropertyChanged(nameof(UpdatePictureFieldRequest));
            }
        }

        public UpdateFieldViewModel(FieldDto fieldDto)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            DeleteFieldCommand = new Command(async () => await OnDeleteFieldCommandAlertYesNoClicked(fieldDto.Id.Value));

            SaveCommand = new Command(async () => await OnSaveCommandAlertYesNoClicked(fieldDto.Id.Value));

            Task.Run(async () => await ExecuteLoadItemsCommand(fieldDto.Id.Value));
        }
        async Task ExecuteLoadItemsCommand(int fieldId)
        {
            IsBusy = true;
            try
            {
                var data = await _fieldServices.GetFieldById(fieldId);
                Name = data.Name;
                Price = (double)data.Price;
                IsOpen = (bool)data.IsOpen;
                Percentage = (double)data.Percentage;
                ImageFieldDto = data.ImageFieldDto;
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
        async Task OnDeleteFieldCommandAlertYesNoClicked(int fieldId)
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "ต้องการที่จะลบจริงๆใช่ไหม ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }
            IsBusy = true;
            var statusSaved = await _fieldServices.DeleteField(fieldId);
            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกลบเรียบร้อยแล้ว", "ตกลง");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถลบข้อมูลได้", "ตกลง");
            }
            await Shell.Current.GoToAsync("..");
        }

        async Task OnSaveCommandAlertYesNoClicked(int fieldId)
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }
            IsBusy = true;

            var request = new UpdateFieldRequest
            {
                Name = Name,
                IsOpen = IsOpen,
                Price = (int)Price,
                Size = Size,
                UpdateDiscountRequest = (UpdateDiscountRequest)UpdateDiscountRequest,
                UpdatePictureFieldRequest = (IList<UpdatePictureFieldRequest>)UpdatePictureFieldRequest
                //ไม่มีสถานะร้าน
            };
            var statusSaved = await _fieldServices.UpdateField(fieldId, request);

            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
