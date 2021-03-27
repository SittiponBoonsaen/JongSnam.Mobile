using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;

namespace JongSnam.Mobile.Services.Implementations
{
    public class PaymentServices : BaseServices, IPaymentServices
    {
        public PaymentServices()
        {

        }
        public async Task<IEnumerable<PaymentDto>> GetPaymentByReservationId(int reservationId)
        {
            var response = await JongSnamServices.GetPaymentByReservationIdWithHttpMessagesAsync(reservationId, CustomHeaders);

            var respondModel = await GetRespondDtoHandlerHttpStatus<IEnumerable<PaymentDto>>(response);

            return respondModel;

        }

        public async Task<bool> CreatePayment(PaymentRequest paymentRequest)
        {
            try
            {
                var response = await JongSnamServices.CreatePaymentWithHttpMessagesAsync(paymentRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePayment(int id, UpdatePaymentRequest updatePaymentRequest)
        {
            try
            {
                var response = await JongSnamServices.UpdatePaymentWithHttpMessagesAsync(id, updatePaymentRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
