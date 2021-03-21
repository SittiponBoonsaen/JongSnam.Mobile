using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;
        public Command DeleteFieldCommand { get; }
        public Command SaveCommand { get; }

        private string _name;
        private double _price;
        private bool _isOpen;
        private double _percentage;
        private IEnumerable<ImageFieldDto> _imageFieldDto;

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
        public IEnumerable<ImageFieldDto> ImageFieldDto
        {
            get => _imageFieldDto;
            set
            {
                _imageFieldDto = value;
                OnPropertyChanged(nameof(ImageFieldDto));
            }
        }

        public UpdateFieldViewModel(FieldDetailDto fieldDetailDto)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            DeleteFieldCommand = new Command(OnDeleteFieldCommandAlertYesNoClicked);

            SaveCommand = new Command(OnSaveCommandAlertYesNoClicked);

            Task.Run(async () => await ExecuteLoadItemsCommand(fieldDetailDto));
        }
        async Task ExecuteLoadItemsCommand(FieldDetailDto fieldDetailDto)
        {
            IsBusy = true;
            try
            {
                Name = fieldDetailDto.Name;
                Price = (double)fieldDetailDto.Price;
                IsOpen = (bool)fieldDetailDto.IsOpen;
                Percentage = (double)fieldDetailDto.Percentage;
                ImageFieldDto = fieldDetailDto.ImageFieldDto;
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
        async void OnDeleteFieldCommandAlertYesNoClicked()
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            if (!answer)
            {
                return;
            }

            await Shell.Current.GoToAsync("..");
        }

        async void OnSaveCommandAlertYesNoClicked()
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            if (!answer)
            {
                return;
            }
            await Shell.Current.GoToAsync("..");
        }
    }
}
