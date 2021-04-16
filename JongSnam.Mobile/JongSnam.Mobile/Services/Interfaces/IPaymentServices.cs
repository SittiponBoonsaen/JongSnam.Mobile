using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Interfaces
{
    public interface IPaymentServices
    {
        Task<IEnumerable<PaymentDto>> GetPaymentByReservationId(int reservationId);

        Task<bool> CreatePayment(PaymentRequest paymentRequest);

        Task<bool> UpdatePayment(int id, UpdatePaymentRequest updatePaymentRequest);
    }
}
