using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.Services.Implementations
{
    public class ReservationServices : BaseServices, IReservationServices
    {
        private readonly IMapperService _mapperService;

        public ReservationServices()
        {
            _mapperService = DependencyService.Get<IMapperService>();
        }
        public async Task<IEnumerable<FieldDtoBasePagingDto>> GetYourReservation(int storeId, int ownerId, int currentPage, int pageSize)
        {
            var response = await JongSnamServices.GetYourReservationWithHttpMessagesAsync(storeId, ownerId, currentPage, pageSize);

            return await GetRespondDtoHandlerHttpStatus<IEnumerable<FieldDtoBasePagingDto>>(response);
        }

    }
}
