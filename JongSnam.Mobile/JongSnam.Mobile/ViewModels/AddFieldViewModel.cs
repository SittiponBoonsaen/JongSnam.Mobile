using System;
using System.Collections.Generic;
using System.Text;
using JongSnam.Mobile.Services.Interfaces;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class AddFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;
        public AddFieldViewModel()
        {
            _fieldServices = DependencyService.Get<IFieldServices>();
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
