using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IReservationServices
    {
        Task<IEnumerable<ReservationDto>> GetYourReservation(int userId, int currentPage, int pageSize);

        Task<IEnumerable<ReservationDto>> GetReservationBySearch(int userId, int startTimeYear, int startTimeMonth, int startTimeDay, int startTimeHour, int startTimeMinute, int startTimeSecond, int stopTimeYear, int stopTimeMonth, int stopTimeDay, int stopTimeHour, int stopTimeMinute, int stopTimeSecond, string userName, string storeName, int currentPage, int pageSize);

        Task<ReservationDetailDto> GetShowDetailYourReservation(int id);

        Task<bool> UpdateApprovalStatus(int id, ReservationApprovalRequest reservationApprovalRequest);

        Task<bool> CreateReservation(ReservationRequest reservationRequest);

        Task<bool> DeleteReservation(int id);

    }
}
