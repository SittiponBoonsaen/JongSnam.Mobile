using System;
using System.Collections.Generic;
using System.Text;

namespace JongSnam.Mobile.ViewModels
{
    public class GraphViewModel : BaseViewModel
    {
        public GraphViewModel()
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
