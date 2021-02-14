using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
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

    }
}
