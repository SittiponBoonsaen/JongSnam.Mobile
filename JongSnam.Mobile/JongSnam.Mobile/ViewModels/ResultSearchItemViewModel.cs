using System;
using System.Collections.Generic;
using System.Text;
using JongSnamService.Models;

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
