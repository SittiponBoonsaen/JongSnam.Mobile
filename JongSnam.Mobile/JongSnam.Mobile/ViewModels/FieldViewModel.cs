using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class FieldViewModel : BaseViewModel
    {
        private readonly IReservationServices _reservationServices;

        public FieldViewModel(FieldDto fieldDto)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
