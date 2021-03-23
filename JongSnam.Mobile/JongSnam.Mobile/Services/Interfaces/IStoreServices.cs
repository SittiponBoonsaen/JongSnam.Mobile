using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
using JongSnamServices.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IStoreServices
    {
        Task<IEnumerable<StoreDto>> GetStores(int currentPage, int pageSize);

        Task<StoreDetailDto> GetStoreById(int id);

        Task<bool> AddStore(StoreRequest storeRequest);

        Task<bool> UpdateStore(int id, UpdateStoreRequest updateStoreRequest);

        Task<IEnumerable<YourStore>> GetYourStores(int ownerId, int currentPage, int pageSize);
    }
}
