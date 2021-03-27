using System;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.SqliteRepository.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.Services.Implementations
{
    public class AuthenticationServices : BaseServices, IAuthenticationServices
    {
        private readonly IRepository<UserModel> _repository;
        private const int MillisecondsDelay = 400;

        public AuthenticationServices()
        {
            _repository = DependencyService.Get<IRepository<UserModel>>();
        }

        public async Task<bool> Login(string user, string password)
        {
            try
            {
                var response = await JongSnamServices.LoginWithHttpMessagesAsync(user, password);
                var userModel = await GetRespondDtoHandlerHttpStatus<UserModel>(response);
                if (userModel == null)
                {
                    return false;
                }
                else
                {
                    // save access token
                    Preferences.Set(AuthorizeConstants.IdKey, userModel.Id);
                    Preferences.Set(AuthorizeConstants.UserKey, userModel.Email);
                    Preferences.Set(AuthorizeConstants.PasswordKey, userModel.Password);
                    await Task.Delay(MillisecondsDelay);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Logut()
        {
            try
            {
                var id = Preferences.Get(AuthorizeConstants.IdKey, string.Empty);
                var response = await JongSnamServices.LogoutWithHttpMessagesAsync(Convert.ToInt32(id));

                // remove
                Preferences.Remove(AuthorizeConstants.UserKey);
                Preferences.Remove(AuthorizeConstants.PasswordKey);


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
