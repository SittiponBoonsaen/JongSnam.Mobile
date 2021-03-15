using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IReservationServices
    {
        Task<IEnumerable<FieldDtoBasePagingDto>> GetYourReservation(int storeId, int ownerId, int currentPage, int pageSize);


    }
}
