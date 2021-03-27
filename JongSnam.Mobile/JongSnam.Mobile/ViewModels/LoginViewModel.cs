using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationServices _authenticationServices;

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            _authenticationServices = DependencyService.Get<IAuthenticationServices>();

            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            var aa = await _authenticationServices.Login("ss", "aa");
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(YourReservationPage)}");
        }
    }
}
