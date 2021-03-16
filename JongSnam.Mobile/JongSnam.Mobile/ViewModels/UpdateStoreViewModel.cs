using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class UpdateStoreViewModel : BaseViewModel
    {
        private readonly IStoreServices _storeServices;

        public Command LoadItemsCommand { get; }

        public UpdateStoreViewModel()
        {
            _storeServices = DependencyService.Get<IStoreServices>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            throw new NotImplementedException();
        }


        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
