using System;
using System.Collections.Generic;
using System.Text;

namespace JongSnam.Mobile.ViewModels
{
    public class ReviewViewModel : BaseViewModel
    {
        public ReviewViewModel(int storeId)
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
