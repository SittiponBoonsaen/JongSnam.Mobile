using JongSnamServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IUsersServices
    {
        Task<IEnumerable<UserDto>> GetUsers();

        Task<UserDto> GetUserById(int id);

        Task<bool> CreateUser(UserRequest userRequest);

        Task<bool> ChangePassword(int id, ChangePasswordRequest changePasswordRequest);

        Task<bool> UpdateUser(int id, UpdateUserRequest updateUserRequest);
    }
}
