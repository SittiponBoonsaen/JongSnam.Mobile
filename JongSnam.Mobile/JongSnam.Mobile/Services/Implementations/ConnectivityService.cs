using JongSnam.Mobile.Services.Interfaces;
using Xamarin.Essentials;

namespace JongSnam.Mobile.Services.Implementations
{
    public class ConnectivityService : IConnectivityService
    {
        /// <summary>
        /// Gets the network access.
        /// </summary>
        /// <returns cref="{NetworkAccess}">Get current network access.</returns>
        public NetworkAccess GetNetworkAccess()
        {
            var current = Connectivity.NetworkAccess;
            return current;
        }

        /// <summary>
        /// Determines whether [is internet connection].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is internet connection]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInternetConnection()
        {
            var networkAccess = GetNetworkAccess();
            if (networkAccess == NetworkAccess.Internet)
            {
                return true;
            }

            return false;
        }
    }
}
