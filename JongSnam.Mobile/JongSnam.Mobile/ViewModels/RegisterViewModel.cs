using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

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
        private IsOpen _userType;

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
        public IsOpen UserType
        {
            get => _userType;
            set
            {
                _userType = value;
                OnPropertyChanged(nameof(UserType));
            }
        }

        private IUsersServices _usersServices;

        public Command RegisterCommand { get; }

        public RegisterViewModel()
        {
            _usersServices = DependencyService.Get<IUsersServices>();

            RegisterCommand = new Command(async () => await OnRegisterCommand());

        }

        async Task OnRegisterCommand()
        {
            if (Password != ConfrimPassword)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "รหัสผ่านไม่ตรงกัน", "ตกลง");
                return;
            }
            var request = new UserRequest
            {
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfrimPassword,
                FirstName = FirstName,
                LastName = LastName,
                ContactMobile = Phone,
                Address = Address,
                ImageProfile = null,
                UserTypeId = UserType.UserTypeId,

            };

            bool answer = await Shell.Current.DisplayAlert("ยืนยันข้อมูล", "คุณแน่ใจที่จะสมัครสมาชิกใช่ไหม ?", "ใช่", "ไม่");
            if (!answer)
            {
                return;
            }

            var statusSaved = await _usersServices.CreateUser(request);

            if (statusSaved)
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "ท่านได้สมัครสมาชิกเรียบร้อยแล้ว", "ตกลง");
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "เกิดข้อผิดพลาดบางอย่าง!!!!", "ตกลง");
            }
           
            
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
