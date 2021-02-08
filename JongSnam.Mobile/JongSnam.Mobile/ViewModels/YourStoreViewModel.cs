using JongSnam.Mobile.Models;
using JongSnam.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourStoreViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }

        public Command LoadItemsCommand { get; }

        public Command AddStoreCommand { get; }
        public YourStoreViewModel()
        {
            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            AddStoreCommand = new Command(OnAddStore);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = new ObservableCollection<Item> {
                    new Item{
                        Text = "test",
                        Description = "ggg"
                    },
                    new Item
                    {
                        Text = "gad",
                        Description = "gfgsdf"
                    }
                };

                foreach (var item in items)
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

        async void OnAddStore(object obj)
        {
            await Shell.Current.GoToAsync(nameof(AddStorePage));
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
