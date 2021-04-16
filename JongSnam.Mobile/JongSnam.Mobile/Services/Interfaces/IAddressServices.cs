using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IAddressServices
    {
        Task<IEnumerable<ProvinceModel>> GetProvinces();

        Task<ProvinceModel> GetProvinceById(int id);

        Task<IEnumerable<DistrictModel>> GetDistrictByProvinceId(int ProvinceId);

        Task<DistrictModel> GetDistrictById(int id);

        Task<IEnumerable<SubDistrictModel>> GetSubDistrictByDistrictId(int DistrictId);

        Task<SubDistrictModel> GetSubDistrictById(int id);
    }
}
