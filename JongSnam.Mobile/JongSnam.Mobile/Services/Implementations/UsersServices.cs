using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.CustomErrors;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Implementations
{
    public class UsersServices : BaseServices, IUsersServices
    {
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var response = await JongSnamServices.GetUsersWithHttpMessagesAsync(CustomHeaders);
            return await GetRespondDtoHandlerHttpStatus<IEnumerable<UserDto>>(response);
        }

        public async Task GetUserById(int id, Action<UserDto> executeSuccess = null, Action<string, Exception> executeError = null)
        {
            try
            {
                var result = await InvokeServiceCheckInternetConnection(async () =>
                {
                    var response = await JongSnamServices.GetUserByIdWithHttpMessagesAsync(id, CustomHeaders);
                    return await GetRespondDtoHandlerHttpStatus<UserDto>(response);
                });

                executeSuccess?.Invoke(result);
            }
            catch (InvalidAccessTokenException ex)
            {
                executeError?.Invoke(ex.Message, ex);
            }
            catch (BadRequestException ex)
            {
                executeError?.Invoke(MessageConstants.SomeValueINvalid + "\n[" + ex.Message + "]", ex);
            }
            catch (UnauthorizedException uex)
            {
                executeError?.Invoke(MessageConstants.UnauthorizedError, uex);
            }
            catch (InternalServerErrorException inex)
            {
                executeError?.Invoke(MessageConstants.SomethingWentWrong, inex);
            }
            catch (NoInternetConnectionException nicex)
            {
                executeError?.Invoke(MessageConstants.CannotConnectToInternet, nicex);
            }
            catch (Exception ex)
            {
                executeError?.Invoke(MessageConstants.SomethingWentWrong, ex);
            }
        }

        public async Task<bool> CreateUser(UserRequest userRequest)
        {
            try
            {
                var response = await JongSnamServices.CreateUserWithHttpMessagesAsync(userRequest);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }
        }

        public async Task<bool> UpdateUser(int id, UpdateUserRequest updateUserRequest)
        {
            try
            {
                var response = await JongSnamServices.UpdateUserWithHttpMessagesAsync(id, updateUserRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }
        }

        public async Task<bool> ChangePassword(int id, ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var response = await JongSnamServices.ChangePasswordWithHttpMessagesAsync(id, changePasswordRequest);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;

            }
        }
    }
}
