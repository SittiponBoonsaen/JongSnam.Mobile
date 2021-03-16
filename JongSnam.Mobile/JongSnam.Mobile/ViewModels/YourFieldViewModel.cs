using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;

        public Command LoadItemsCommand { get; }
        public Command UpdateFieldCommand { get; }
        public ObservableCollection<FieldDto> Items { get; }

        public YourFieldViewModel()
        {

            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<FieldDto>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            UpdateFieldCommand = new Command<FieldDto>(OnUpdateField);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var data = await _fieldServices.GetFieldByStoreId(5, 1, 3);
                foreach (var item in data)
                {
                    Items.Add(item);
                }

            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        async void OnUpdateField(FieldDto fieldDto)
        {
            await Shell.Current.GoToAsync(nameof(UpdateFieldPage));
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
