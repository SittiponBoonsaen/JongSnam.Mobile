using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourProFileViewModel : BaseViewModel
    {

        private readonly IUsersServices _usersServices; 

        public Command ChangePasswordCommand { get; }

        public YourProFileViewModel(int id)
        {
            _usersServices = DependencyService.Get<IUsersServices>();
            ChangePasswordCommand = new Command(OnChangePassword);
        }

        async void OnChangePassword(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ChangePasswordPage));
        }
        public async void OnAppearing()
        {
            IsBusy = true;
            var aa = await _usersServices.GetUsers();
        }
    }
}
