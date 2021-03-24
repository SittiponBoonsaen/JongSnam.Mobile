using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ListFieldViewModel : BaseViewModel
    {
        private readonly IFieldServices _fieldServices;

        private string _storeName;
        private double _rating;
        private string _officeHours;
        private FieldDto _selectedItem;

        public Command LoadFieldCommand { get; }
        public Command<FieldDto> ItemTapped { get; }
        public Command ReviewCommand { get; }

        public ObservableCollection<FieldDto> Items { get; }

        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName =  value;
                OnPropertyChanged(nameof(StoreName));
            }
        }

        public double Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged(nameof(Rating));
            }
        }

        public string OfficeHours
        {
            get => _officeHours;
            set
            {
                _officeHours = value;
                OnPropertyChanged(nameof(OfficeHours));
            }
        }

        public FieldDto SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }



        public ListFieldViewModel(StoreDto storeDto)
        {
            _fieldServices = DependencyService.Get<IFieldServices>();

            Items = new ObservableCollection<FieldDto>();

            LoadFieldCommand = new Command(async () => await ExecuteLoadFieldCommand(storeDto.Id.Value));

            ItemTapped = new Command<FieldDto>(OnItemSelected);

            ReviewCommand = new Command(async() => await OnReview(storeDto.Id.Value));

            Task.Run(async () => await ExecuteLoadItemsCommand(storeDto));

        }

        async Task ExecuteLoadItemsCommand(StoreDto storeDto)
        {
            IsBusy = true;
            try
            {
                StoreName = storeDto.Name;
                Rating = (double)storeDto.Rating;
                OfficeHours = storeDto.OfficeHours;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadFieldCommand(int storeId)
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var items = await _fieldServices.GetFieldByStoreId(storeId, 1, 20);
                
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async void OnItemSelected(FieldDto fieldDto)
        {
            if (fieldDto == null)
            {
                return;
            }
            await Shell.Current.Navigation.PushAsync(new FieldPage(fieldDto));
        }

        async Task OnReview(int storeId)
        {
            await Shell.Current.Navigation.PushAsync(new ReviewPage(storeId));
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
