using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IUsersServices
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task GetUserById(int id, Action<UserDto> executeSuccess = null, Action<string, Exception> executeError = null);

        Task<bool> CreateUser(UserRequest userRequest);

        Task<bool> ChangePassword(int id, ChangePasswordRequest changePasswordRequest);

        Task<bool> UpdateUser(int id, UpdateUserRequest updateUserRequest);
    }
}
