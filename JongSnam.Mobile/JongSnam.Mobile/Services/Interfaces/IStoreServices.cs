using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IStoreServices
    {
        Task<IEnumerable<YourStore>> GetStores(int currentPage, int pageSize);

        Task<bool> AddStore(StoreRequest storeRequest);

        Task<bool> UpdateStore(int id, UpdateStoreRequest updateStoreRequest);

        Task<IEnumerable<YourStore>> GetYourStores(int ownerId, int currentPage, int pageSize);
    }
}
