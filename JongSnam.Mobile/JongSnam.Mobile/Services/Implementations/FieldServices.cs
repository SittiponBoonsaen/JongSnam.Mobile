using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Implementations
{
    public class FieldServices : BaseServices, IFieldServices
    {
        public FieldServices()
        {

        }

        public async Task<IEnumerable<FieldDto>> GetFieldBySearch(double startPrice, double toPrice, int districtId, int provinceId, int currentPage, int pageSize)
        {
            var response = await JongSnamServices.GetFieldBySearchWithHttpMessagesAsync(startPrice, toPrice, districtId, provinceId, currentPage, pageSize, CustomHeaders);

            var ReservationDto = await GetRespondDtoHandlerHttpStatus<FieldDtoBasePagingDto>(response);

            return ReservationDto.Collection;
        }

        public async Task<FieldDetailDto> GetFieldById(int id)
        {
            var response = await JongSnamServices.GetFieldByIdWithHttpMessagesAsync(id, CustomHeaders);

            var ReservationDto = await GetRespondDtoHandlerHttpStatus<FieldDetailDto>(response);

            return ReservationDto;
        }

        public async Task<IEnumerable<FieldDto>> GetFieldByStoreId(int id, int currentPage, int pageSize)
        {
            var response = await JongSnamServices.GetFieldByStoreWithHttpMessagesAsync(id, currentPage, pageSize, CustomHeaders);

            var ReservationDto = await GetRespondDtoHandlerHttpStatus<FieldDtoBasePagingDto>(response);

            return ReservationDto.Collection;
        }

        public async Task<bool> AddField(AddFieldRequest addFieldRequest)
        {
            try
            {
                var response = await JongSnamServices.AddFieldWithHttpMessagesAsync(addFieldRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateField(int id, UpdateFieldRequest updateFieldRequest)
        {
            try
            {
                var response = await JongSnamServices.UpdateFieldWithHttpMessagesAsync(id, updateFieldRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteField(int id)
        {
            try
            {
                var response = await JongSnamServices.DeleteFieldWithHttpMessagesAsync(id, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
