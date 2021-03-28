using System;
using System.Collections.Generic;
using System.Text;
using JongSnam.Mobile.Models;

namespace JongSnam.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _phone;
        private string _address;
        private string _emailName;
        private string _lastName;
        private string _firstName;
        private string _password;
        private string _confrimPassword;
        private string _userType;

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
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ConfrimPassword
        {
            get => _confrimPassword;
            set
            {
                _confrimPassword = value;
                OnPropertyChanged(nameof(ConfrimPassword));
            }
        }
        public List<IsOpen> UserTypes { get; set; } = new List<IsOpen>()
        {
        new IsOpen(){Name = "เจ้าของร้าน", UserTypeId = 1},
        new IsOpen(){Name = "ลูกค้า", UserTypeId = 2},
        };
        public string UserType
        {
            get => _userType;
            set
            {
                _userType = value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        public RegisterViewModel()
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
