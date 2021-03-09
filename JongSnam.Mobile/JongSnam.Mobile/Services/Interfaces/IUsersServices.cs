using JongSnamService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IUsersServices
    {
        Task<IEnumerable<UserDto>> GetUsers();
    }
}
