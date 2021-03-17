using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IAddressServices
    {
        Task<IEnumerable<ProvinceModel>> GetProvinces();

        Task<IEnumerable<DistrictModel>> GetDistrictByProvinceId(int ProvinceId);

        Task<IEnumerable<SubDistrictModel>> GetSubDistrictByDistrictId(int DistrictId);
    }
}
