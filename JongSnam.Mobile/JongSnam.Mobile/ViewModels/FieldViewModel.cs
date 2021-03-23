using System;
using System.Collections.Generic;
using System.Text;
using JongSnamService.Models;

namespace JongSnam.Mobile.ViewModels
{
    public class FieldViewModel : BaseViewModel
    {
        public FieldViewModel(FieldDto fieldDto)
        {

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
