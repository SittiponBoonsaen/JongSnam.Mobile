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
        private const int MillisecondsDelay = 400;

        public AuthenticationServices()
        {
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
                    Preferences.Clear();

                    // save access token
                    Preferences.Set(AuthorizeConstants.UserIdKey, userModel.Id.ToString());
                    Preferences.Set(AuthorizeConstants.UserKey, userModel.Email);
                    Preferences.Set(AuthorizeConstants.PasswordKey, userModel.Password);
                    Preferences.Set(AuthorizeConstants.UserTypeKey, userModel.UserType);
                    Preferences.Set(AuthorizeConstants.IsLoggedInKey, userModel.IsLoggedIn.ToString());
                    await Task.Delay(MillisecondsDelay);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Logut()
        {
            try
            {
                var id = Preferences.Get(AuthorizeConstants.UserIdKey, string.Empty);
                var response = await JongSnamServices.LogoutWithHttpMessagesAsync(Convert.ToInt32(id));

                // remove
                Preferences.Clear();


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckLogin()
        {
            try
            {
                var username = Preferences.Get(AuthorizeConstants.UserKey, string.Empty);
                var password = Preferences.Get(AuthorizeConstants.PasswordKey, string.Empty);

                var response = await JongSnamServices.LoginWithHttpMessagesAsync(username, password);

                var userModel = await GetRespondDtoHandlerHttpStatus<UserModel>(response);
                if (userModel == null)
                {
                    return false;
                }
                else
                {
                    Preferences.Clear();

                    // save access token
                    Preferences.Set(AuthorizeConstants.UserIdKey, userModel.Id.ToString());
                    Preferences.Set(AuthorizeConstants.UserKey, userModel.Email);
                    Preferences.Set(AuthorizeConstants.PasswordKey, userModel.Password);
                    Preferences.Set(AuthorizeConstants.UserTypeKey, userModel.UserType);
                    Preferences.Set(AuthorizeConstants.IsLoggedInKey, userModel.IsLoggedIn.ToString());
                    await Task.Delay(MillisecondsDelay);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
