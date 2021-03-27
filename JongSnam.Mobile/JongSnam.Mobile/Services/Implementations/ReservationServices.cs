using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Implementations
{
    public class ReservationServices : BaseServices, IReservationServices
    {

        public ReservationServices()
        {

        }
        public async Task<IEnumerable<ReservationDto>> GetYourReservation(int userId, int currentPage, int pageSize)
        {
            var response = await JongSnamServices.GetYourReservationWithHttpMessagesAsync(userId, currentPage, pageSize, CustomHeaders);

            var ReservationDto = await GetRespondDtoHandlerHttpStatus<ReservationDtoBasePagingDto>(response);

            return ReservationDto.Collection;
        }
        public async Task<IEnumerable<ReservationDto>> GetReservationBySearch(int userId, int startTimeYear, int startTimeMonth, int startTimeDay, int startTimeHour, int startTimeMinute, int startTimeSecond, int stopTimeYear, int stopTimeMonth, int stopTimeDay, int stopTimeHour, int stopTimeMinute, int stopTimeSecond, string userName, string storeName, int currentPage, int pageSize)
        {
            var response = await JongSnamServices.GetReservationBySearchWithHttpMessagesAsync(userId, startTimeYear, startTimeMonth, startTimeDay, startTimeHour, startTimeMinute, startTimeSecond, stopTimeYear, stopTimeMonth, stopTimeDay, stopTimeHour, stopTimeMinute, stopTimeSecond, userName, storeName, currentPage, pageSize, CustomHeaders);

            var ReservationDto = await GetRespondDtoHandlerHttpStatus<ReservationDtoBasePagingDto>(response);

            return ReservationDto.Collection;
        }


        public async Task<ReservationDetailDto> GetShowDetailYourReservation(int id)
        {
            var response = await JongSnamServices.GetShowDetailYourReservationWithHttpMessagesAsync(id, CustomHeaders);

            var ReservationDto = await GetRespondDtoHandlerHttpStatus<ReservationDetailDto>(response);

            return ReservationDto;
        }


        public async Task<bool> UpdateApprovalStatus(int id, ReservationApprovalRequest reservationApprovalRequest)
        {
            try
            {
                var response = await JongSnamServices.UpdateApprovalStatusWithHttpMessagesAsync(id, reservationApprovalRequest, CustomHeaders);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> CreateReservation(ReservationRequest reservationRequest)
        {
            try
            {
                var response = await JongSnamServices.CreateReservationWithHttpMessagesAsync(reservationRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteReservation(int id)
        {
            try
            {
                var response = await JongSnamServices.DeleteReservationWithHttpMessagesAsync(id, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
