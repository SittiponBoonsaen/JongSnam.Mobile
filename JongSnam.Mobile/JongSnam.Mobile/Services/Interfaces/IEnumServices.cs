using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IEnumServices
    {
        Task<List<EnumDto>> GetProvinces();

        Task<List<EnumDto>> GetDistrict(int provinceId);

        Task<List<EnumDto>> GetSubDistrict(int districtId);
    }
}
