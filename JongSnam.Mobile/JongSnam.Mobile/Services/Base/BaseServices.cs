using JongSnam.Mobile.Services.Interfaces;
using JongSnamService;
using Microsoft.Rest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.Services.Base
{
    public abstract class BaseServices
    {
        protected const string AccessTokenKey = "AccessToken";

        protected readonly IConfigurationService ConfigurationService;

        protected readonly JongSnamServices JongSnamServices;

        protected readonly IConnectivityService ConnectivityService;

        protected Dictionary<string, List<string>> CustomHeaders
        {
            get
            {
                var customHeaders = new Dictionary<string, List<string>>();

                customHeaders.Add("Authorization", new List<string> { "Test Token" });

                return customHeaders;
            }
        }

        public BaseServices()
        {
            ConfigurationService = DependencyService.Get<IConfigurationService>();
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            //JongSnamServices = new JongSnamServices(new Uri(ConfigurationService.Configuration.JongSnamServicesUrl), handler);
            JongSnamServices = new JongSnamServices(new Uri("http://172.17.29.65:8080/"), handler);
        }


        protected async Task<T> GetRespondDtoHandlerHttpStatus<T>(HttpOperationResponse httpOperationResponse)
        {
            var responseString = await httpOperationResponse.Response.Content.ReadAsStringAsync();
            //if (httpOperationResponse.Response.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    var respondModel = JsonConvert.DeserializeObject<T>(responseString);
            //    return respondModel;
            //}
            var respondModel = JsonConvert.DeserializeObject<T>(responseString);
            return respondModel;
        }
    }
}
