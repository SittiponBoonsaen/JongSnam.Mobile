using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourProFileViewModel : BaseViewModel
    {

        private readonly IUsersServices _usersServices;

        public UserDto DataUser { get; set; }

        public Command LoadItemsCommand { get; }

        public Command ChangePasswordCommand { get; }

        public YourProFileViewModel(int id)
        {

            DataUser = new UserDto();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(id));

            //ChangePasswordCommand = new Command(OnChangePassword);

            _usersServices = DependencyService.Get<IUsersServices>();
        }
        async Task ExecuteLoadItemsCommand(int id)
        {
            IsBusy = true;
            try
            {
                DataUser = await _usersServices.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }


        async void OnChangePassword(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ChangePasswordPage));
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
