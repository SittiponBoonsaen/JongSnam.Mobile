using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Implementations
{
    public class AddressServices : BaseServices, IAddressServices
    {
        public AddressServices()
        {

        }
        public async Task<IEnumerable<ProvinceModel>> GetProvinces()
        {
            var response = await JongSnamServices.GetProvincesWithHttpMessagesAsync();

            var respondModel = await GetRespondDtoHandlerHttpStatus<IEnumerable<ProvinceModel>>(response);

            return respondModel;

        }
        public async Task<ProvinceModel> GetProvinceById(int id)
        {
            var response = await JongSnamServices.GetProvinceByIdWithHttpMessagesAsync(id, CustomHeaders);
            return await GetRespondDtoHandlerHttpStatus<ProvinceModel>(response);
        }


        public async Task<IEnumerable<DistrictModel>> GetDistrictByProvinceId(int ProvinceId)
        {
            var response = await JongSnamServices.GetDistrictByProvinceIdWithHttpMessagesAsync(ProvinceId);

            var respondModel = await GetRespondDtoHandlerHttpStatus<IEnumerable<DistrictModel>>(response);

            return respondModel;

        }

        public async Task<DistrictModel> GetDistrictById(int id)
        {
            var response = await JongSnamServices.GetDistrictByIdWithHttpMessagesAsync(id, CustomHeaders);
            return await GetRespondDtoHandlerHttpStatus<DistrictModel>(response);
        }


        public async Task<IEnumerable<SubDistrictModel>> GetSubDistrictByDistrictId(int DistrictId)
        {
            var response = await JongSnamServices.GetSubDistrictByDistrictIdWithHttpMessagesAsync(DistrictId);

            var respondModel = await GetRespondDtoHandlerHttpStatus<IEnumerable<SubDistrictModel>>(response);

            return respondModel;

        }

        public async Task<SubDistrictModel> GetSubDistrictById(int id)
        {
            var response = await JongSnamServices.GetSubDistrictByIdWithHttpMessagesAsync(id, CustomHeaders);

            return await GetRespondDtoHandlerHttpStatus<SubDistrictModel>(response);
        }
    }
}
