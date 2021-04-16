using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthenticationServices _authenticationServices;
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

            _authenticationServices = DependencyService.Get<IAuthenticationServices>();
            RegisterCommand = new Command(async () => await OnRegisterCommand());

        }

        async Task OnRegisterCommand()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrWhiteSpace(Address) || UserType.UserTypeId == 0 || UserType == null || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Phone))
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลให้ครบถ้วน", "ตกลง");
                    return;
                }

                if (!IsValidEmail(Email))
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน!", "กรุณากรอกอีเมลให้ถูกต้อง", "ตกลง");
                    return;
                }
                else if (Phone.Length < 10)
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน!", "กรุณากรอกเบอร์โทรให้ครบ10หลัก", "ตกลง");
                    return;
                }

                else if (Password != ConfrimPassword)
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน!", "รหัสผ่านไม่ตรงกัน", "ตกลง");
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


                bool answer = await App.Current.MainPage.DisplayAlert("ยืนยันข้อมูล", "คุณแน่ใจที่จะสมัครสมาชิกใช่ไหม ?", "ใช่", "ไม่");
                if (!answer)
                {
                    return;
                }

                var statusSaved = await _usersServices.CreateUser(request);

                if (statusSaved)
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน!", "ท่านได้สมัครสมาชิกเรียบร้อยแล้ว", "ตกลง");

                    if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                    {
                        return;
                    }

                    var statusLogin = await _authenticationServices.Login(request.Email, request.Password);

                    if (!statusLogin)
                    {
                        await App.Current.MainPage.DisplayAlert("ไม่สามารถเข้าสู่ระบบได้!", "Username หรือ password ไม่ถูกต้อง", "ตกลง");
                        return;
                    }

                    var userType = Preferences.Get(AuthorizeConstants.UserTypeKey, string.Empty);

                    if (userType == "Owner")
                    {
                        IsOwner = true;
                        IsCustomer = false;
                        Application.Current.MainPage = new AppShell();
                        await Shell.Current.GoToAsync($"//{nameof(YourReservationPage)}");

                    }
                    else
                    {
                        IsCustomer = true;
                        IsOwner = false;
                        Application.Current.MainPage = new AppShellCustomer();
                        await Shell.Current.GoToAsync($"//{nameof(YourReservationPage)}");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน!", "เกิดข้อผิดพลาดบางอย่าง!!!!", "ตกลง");
                }

            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("แจ้งเตือน!", "กรุณากรอกข้อมูลให้ครบถ้วน", "ตกลง");
                return;
                throw ex;
            }
            finally
            {
                IsBusy = true;
            }
            
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
