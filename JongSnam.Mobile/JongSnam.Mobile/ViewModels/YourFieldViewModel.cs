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
    public class YourFieldViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; }
        public Command UpdateFieldCommand { get; }
        public ObservableCollection<Item> Items { get; }

        public YourFieldViewModel()
        {
            Items = new ObservableCollection<Item>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            UpdateFieldCommand = new Command<Item>(OnUpdateField);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                var items = new ObservableCollection<Item> {
                    new Item{
                        Text = "สนามที่1",
                        Description = "test"
                    },
                    new Item
                    {
                        Text = "สนามที่2",
                        Description = "hgadf"
                    },
                    new Item
                    {
                        Text = "สนามที่3",
                        Description = "ดกhafdเหก"
                    },
                    new Item
                    {
                        Text = "สนามที่4",
                        Description = "ahdf"
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
        async void OnUpdateField(Item Item)
        {
            await Shell.Current.GoToAsync(nameof(UpdateFieldPage));
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
