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
        private string _emailName;
        private string _phone;
        private string _address;

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
            get => _emailName;
            set
            {
                _emailName = value;
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

            SaveCommand = new Command(async () => await ExecuteSaveCommand(id));
        }

        async Task ExecuteLoadItemsCommand(int id)
        {
            IsBusy = true;
            try
            {
                var dataUser = await _usersServices.GetUserById(id);
                FirstName = dataUser.FirstName;
                LastName = dataUser.LastName;
                Email = dataUser.Email;
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

        async Task ExecuteSaveCommand(int id)
        {
            bool answer = await Shell.Current.DisplayAlert("แจ้งเตือน!", "ต้องการที่จะแก้ไขจริงๆใช่ไหม ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }

            var request = new UpdateUserRequest
            {
                LastName = LastName,
                FirstName = FirstName,
                Email = Email,
                ContactMobile = Phone,
                Address = Address
            };

            var statusSaved = await _usersServices.UpdateUser(id, request);

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
