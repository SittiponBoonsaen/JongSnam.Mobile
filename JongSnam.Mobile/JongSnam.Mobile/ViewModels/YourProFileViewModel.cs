using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourProFileViewModel : BaseViewModel
    {

        private readonly IUsersServices _usersServices;

        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _address;
        private string _email;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
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

        public UserDto DataUser { get; set; }

        public Command LoadItemsCommand { get; }

        public Command ChangePasswordCommand { get; }
        public Command SaveCommand { get; }

        public YourProFileViewModel(int id)
        {
            _usersServices = DependencyService.Get<IUsersServices>();

            DataUser = new UserDto();

            Task.Run(async () => await ExecuteLoadItemsCommand(id));

            SaveCommand = new Command(async () => await ExecuteSaveCommand());
        }

        async Task ExecuteLoadItemsCommand(int id)
        {
            IsBusy = true;
            try
            {
                var dataUser = await _usersServices.GetUserById(id);
                FirstName = dataUser.FirstName;
                Email = dataUser.Email;
                LastName = dataUser.LastName;
                Phone = dataUser.ContactMobile;
                Address = dataUser.Address;
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

        async Task ExecuteSaveCommand()
        {
            var request = new UserRequest
            {
                LastName = LastName,
                FirstName = FirstName,
                ContactMobile = Phone,
                Address = Address
            };

            var statusSaved = await _usersServices.UpdateUser(request);

            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ข้อมูลถูกบันทึกเรียบร้อยแล้ว", "ตกลง");
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่สามารถบันทึกข้อมูลได้", "ตกลง");
            }
        }


        async void OnChangePassword(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ChangePasswordPage));
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
