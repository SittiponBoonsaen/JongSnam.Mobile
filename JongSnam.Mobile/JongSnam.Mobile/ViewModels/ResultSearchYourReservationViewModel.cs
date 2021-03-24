using System;
using System.Collections.Generic;
using System.Text;

namespace JongSnam.Mobile.ViewModels
{
    public class ResultSearchYourReservationViewModel : BaseViewModel
    {
        public ResultSearchYourReservationViewModel()
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
