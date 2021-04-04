using System;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Validations;
using JongSnam.Mobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationServices _authenticationServices;

        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;

        public Command LoginCommand { get; private set; }
        public Command RegisterCommand { get; set; }
        public Command EmailTextChangedCommand { get; private set; }
        public Command PasswordTextChangedCommand { get; private set; }

        public ValidatableObject<string> Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginViewModel()
        {
            // remove all storage that we was wrote
            Preferences.Clear();

            _authenticationServices = DependencyService.Get<IAuthenticationServices>();

            AddValidations();

            SetupCommands();
        }

        void AddValidations()
        {
            _email = new ValidatableObject<string>();
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "กรุณากรอก อีเมลล์" });
            _email.Validations.Add(new IsEmailRule { ValidationMessage = "รูปแบบของ อีเมลล์ไม่ถูกต้อง" });

            _password = new ValidatableObject<string>();
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "กรุณากรอก พาสเวิด" });
        }

        private bool IsValidLogin()
        {
            return _email.Validate() & _password.Validate();
        }

        void SetupCommands()
        {
            LoginCommand = new Command(async () => await ExecuteLoginCommand());

            RegisterCommand = new Command(async () => await OnRegisterCommand());


            EmailTextChangedCommand = new Command(() => _email.Validate());
            PasswordTextChangedCommand = new Command(() => _password.Validate());
        }

        async Task OnRegisterCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        async Task ExecuteLoginCommand()
        {
            if (!IsValidLogin())
            {
                return;
            }
            IsBusy = true;

            var statusLogin = await _authenticationServices.Login(Email.Value, Password.Value);
            IsBusy = false;
            if (!statusLogin)
            {
                await App.Current.MainPage.DisplayAlert("ไม่สามารถเข้าสู่ระบบได้!", "Email หรือ password ไม่ถูกต้อง", "ตกลง");
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
            else if (userType == "Customer")
            {
                IsCustomer = true;
                IsOwner = false;
                Application.Current.MainPage = new AppShellCustomer();
                await Shell.Current.GoToAsync($"//{nameof(YourReservationPage)}");
            }
            else
            {

            }
        }
    }
}
