using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamServices.Models;

namespace JongSnam.Mobile.Services.Implementations
{
    public class ReviewServices : BaseServices, IReviewServices
    {
        public async Task<SumaryRatingDto> GetReviewByStoreId(int storeId, int currentPage, int pageSize)
        {
            var response = await JongSnamServices.GetReviewByStoreIdWithHttpMessagesAsync(storeId, currentPage, pageSize, CustomHeaders);

            return await GetRespondDtoHandlerHttpStatus<SumaryRatingDto>(response);
        }

        public async Task<bool> AddReview(ReviewRequest reviewRequest)
        {
            try
            {
                var response = await JongSnamServices.AddReviewWithHttpMessagesAsync(reviewRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
