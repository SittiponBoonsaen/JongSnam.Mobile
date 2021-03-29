using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ResultSearchItemViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;

        public ObservableCollection<YourFieldModel> Items { get; }
        public Command<YourFieldModel> ItemTapped { get; }
        public Command LoadItemsCommand { get; }
        public string IsOpenString { get; private set; }

        public ResultSearchItemViewModel(double startPrice, double toPrice, int? districtId, int? provinceId)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<YourFieldModel>();

            ItemTapped = new Command<YourFieldModel>(OnItemSelected);

            int? pro = provinceId == 0 || provinceId == null ? 0 : provinceId;
            if (pro == 0)
            {
                pro = null;
            }

            int? dis = districtId == 0 || districtId == null ? 0 : districtId;
            if (dis == 0)
            {
                dis = null;
            }

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(startPrice, toPrice, dis, pro));
        }
        async Task ExecuteLoadItemsCommand(double startPrice, double toPrice, int? districtId, int? provinceId)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _fieldServices.GetFieldBySearch(startPrice, toPrice, districtId, provinceId, 1, 6);
                foreach (var item in items)
                {
                    Items.Add(new YourFieldModel
                    {
                        Id = item.StoreModel.Id,
                        StoreName = item.StoreName,
                        IsOpenString = item.IsOpen == true ? IsOpenString="เปิดบริการ" : IsOpenString = "ปิดบริการ",
                        Price = item.Price,
                        StoredtoModel = item.StoreModel,
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.ImageFieldModel[0].Image)))
                    });
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
        private async void OnItemSelected(YourFieldModel yourFieldModel)
        {
           
            var data = new StoreDtoModel
            {
                Id = yourFieldModel.StoredtoModel.Id,
                Name = yourFieldModel.StoredtoModel.Name,
                Rating = null,
                OfficeHours = yourFieldModel.StoredtoModel.OfficeHours,
                IsOpen = yourFieldModel.StoredtoModel.IsOpen,
                Image = yourFieldModel.StoredtoModel.Image
            };
            await Shell.Current.Navigation.PushAsync(new ListFieldPage(data));
        }


        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
