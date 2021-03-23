using System;
using System.Collections.Generic;
using System.Text;
using JongSnamServices.Models;

namespace JongSnam.Mobile.ViewModels
{
    public class ResultSearchItemViewModel : BaseViewModel
    {
        public ResultSearchItemViewModel(FieldDto fieldDto)
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
