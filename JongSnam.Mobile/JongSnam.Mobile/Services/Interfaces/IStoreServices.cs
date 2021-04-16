using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamService.Models;

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
