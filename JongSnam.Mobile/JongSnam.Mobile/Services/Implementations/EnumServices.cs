using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Implementations
{
    public class EnumServices : BaseServices, IEnumServices
    {
        public async Task<List<EnumDto>> GetDistrict(int provinceId)
        {
            var response = await JongSnamServices.GetEnumsWithHttpMessagesAsync("District", provinceId, CustomHeaders);

            var respondModel = await GetRespondDtoHandlerHttpStatus<List<EnumDto>>(response);

            return respondModel;
        }

        public async Task<List<EnumDto>> GetProvinces()
        {
            var response = await JongSnamServices.GetEnumsWithHttpMessagesAsync("Province", null, CustomHeaders);

            var respondModel = await GetRespondDtoHandlerHttpStatus<List<EnumDto>>(response);

            return respondModel;

        }
        public async Task<List<EnumDto>> GetSubDistrict(int districtId)
        {
            var response = await JongSnamServices.GetEnumsWithHttpMessagesAsync("SubDistrict", districtId, CustomHeaders);

            var respondModel = await GetRespondDtoHandlerHttpStatus<List<EnumDto>>(response);

            return respondModel;

        }
    }
}
