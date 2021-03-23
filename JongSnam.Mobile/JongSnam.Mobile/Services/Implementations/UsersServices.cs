using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JongSnam.Mobile.Services.Implementations
{
    public class UsersServices : BaseServices, IUsersServices
    {
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var response = await JongSnamServices.GetUsersWithHttpMessagesAsync(CustomHeaders);
            return await GetRespondDtoHandlerHttpStatus<IEnumerable<UserDto>>(response);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var response = await JongSnamServices.GetUserByIdWithHttpMessagesAsync(id, CustomHeaders);
            return await GetRespondDtoHandlerHttpStatus<UserDto>(response);
        }

        public async Task<bool> CreateUser(UserRequest userRequest)
        {
            try
            {
                var response = await JongSnamServices.CreateUserWithHttpMessagesAsync(userRequest);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(int id, UpdateUserRequest updateUserRequest)
        {
            try
            {
                var response = await JongSnamServices.UpdateUserWithHttpMessagesAsync(id , updateUserRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
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
            }
        }
    }
}
