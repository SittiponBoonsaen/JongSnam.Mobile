using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationServices _authenticationServices;

        private string _userName;
        private string _password;

        public Command LoginCommand { get; }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
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

        public LoginViewModel()
        {
            _authenticationServices = DependencyService.Get<IAuthenticationServices>();

            LoginCommand = new Command(async () => await ExecuteLoginCommand());
        }

        async Task ExecuteLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            var statusLogin = await _authenticationServices.Login(UserName, Password);

            if (!statusLogin)
            {
                await Shell.Current.DisplayAlert("ไม่สามารถเข้าสู่ระบบได้!", "Username หรือ password ไม่ถูกต้อง", "ตกลง");
                return;
            }

            var userType = Preferences.Get(AuthorizeConstants.UserTypeKey, string.Empty);

            if (userType == "Owner")
            {
                await Shell.Current.Navigation.PushAsync(new YourReservationPage());
            }
            else
            {

            }
        }
    }
}
