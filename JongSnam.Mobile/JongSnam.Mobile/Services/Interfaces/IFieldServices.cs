using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IFieldServices
    {
        Task<IEnumerable<FieldDto>> GetFieldBySearch(double? startPrice, double? toPrice, int? districtId, int? provinceId, int currentPage, int pageSize);

        Task<FieldDetailDto> GetFieldById(int id);

        Task<IEnumerable<FieldDto>> GetFieldByStoreId(int id, int currentPage, int pageSize);

        Task<bool> AddField(AddFieldRequest addFieldRequest);

        Task<bool> UpdateField(int id, UpdateFieldRequest updateFieldRequest);

        Task<bool> DeleteField(int id);
    }
}
