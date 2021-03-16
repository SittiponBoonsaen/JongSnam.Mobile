﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Base;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.Services.Implementations
{
    public class StoreServices : BaseServices, IStoreServices
    {
        private readonly IMapperService _mapperService;

        public StoreServices()
        {
            _mapperService = DependencyService.Get<IMapperService>();
        }
        public async Task<IEnumerable<YourStore>> GetStores(int currentPage, int pageSize)
        {
            var response = await JongSnamServices.GetStoresWithHttpMessagesAsync(currentPage, pageSize, CustomHeaders);

            return await GetRespondDtoHandlerHttpStatus<IEnumerable<YourStore>>(response);
        }

        public async Task<bool> AddStore(StoreRequest storeRequest)
        {
            try
            {
                var response = await JongSnamServices.AddStoreWithHttpMessagesAsync(storeRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStore(int id, UpdateStoreRequest updateStoreRequest)
        {
            try
            {
                var response = await JongSnamServices.UpdateStoreWithHttpMessagesAsync(id, updateStoreRequest, CustomHeaders);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<YourStore>> GetYourStores(int ownerId, int currentPage, int pageSize)
        {

            var response = await JongSnamServices.GetYourStoresWithHttpMessagesAsync(ownerId, currentPage, pageSize, CustomHeaders);

            var respondModel =  await GetRespondDtoHandlerHttpStatus<YourStoreBasePagingDto>(response);

            return respondModel.Collection;

        }


    }
}