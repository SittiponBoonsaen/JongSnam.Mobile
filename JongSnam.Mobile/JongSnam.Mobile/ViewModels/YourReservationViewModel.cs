using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class YourReservationViewModel : BaseViewModel
    {

        private readonly IReservationServices _reservationServices;
        private string _textMonth;
        private string _textYear;

        public ObservableCollection<ReservationDto> Items { get; }
        public Command SearchReservationCommand { get; }
        public Command MonthGraphCommand { get; }
        public Command YearGraphCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command<ReservationDto> ItemTapped { get; }
        public string ApprovalStatusString { get; private set; }
        public string TextMonth
        {
            get => _textMonth;
            set
            {
                _textMonth = value;
                OnPropertyChanged(nameof(TextMonth));
            }
        }
        public string TextYear
        {
            get => _textYear;
            set
            {
                _textYear = value;
                OnPropertyChanged(nameof(TextYear));
            }
        }
        public YourReservationViewModel()
        {
            Title = "การจองของคุณ";

            _reservationServices = DependencyService.Get<IReservationServices>();

            Items = new ObservableCollection<ReservationDto>();

            SearchReservationCommand = new Command(OnSearchReservation);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<ReservationDto>(OnItemSelected);

            var userType = Preferences.Get(AuthorizeConstants.UserTypeKey, string.Empty);
            if (userType == "Owner")
            {
                TextMonth = "ดูกราฟรายเดือน";
                TextYear = "ดูกราฟรายปี";

                MonthGraphCommand = new Command(async () => await OnMonthGraph(Items));

                YearGraphCommand = new Command(async () => await OnYearGraph(Items));
            }

            IsBusy = false;
        }

        async Task OnMonthGraph(ObservableCollection<ReservationDto> items)
        {

            await Shell.Current.Navigation.PushAsync(new MonthGraphPage(items));
        }

        async Task OnYearGraph(ObservableCollection<ReservationDto> items)
        {

            await Shell.Current.Navigation.PushAsync(new YearGraphPage(items));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();



                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, null);
                var items = await _reservationServices.GetYourReservation(Convert.ToInt32(userId), 1, 20);
                if (items == null)
                {
                    IsBusy = false;
                    return;
                }
                foreach (var item in items)
                {
                    Items.Add(
                    new YourReservationModel
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                        StoreName = item.StoreName,
                        ContactMobile = item.ContactMobile,
                        StartTimePicker = item.StartTime.Value.TimeOfDay,
                        StopTimePicker = item.StopTime.Value.TimeOfDay,
                        IsApproved = item.ApprovalStatus.Value ? true : false,
                        UnApproved = item.ApprovalStatus == false ? true : false,
                        ApprovalStatusString = GetApprovalStatus(item.ApprovalStatus.Value, item.CreatedDate.Value),
                        DateTime = item.StartTime.Value.Date,
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Image)))
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //await Shell.Current.GoToAsync("//LoginPage");
            }
            finally
            {
                IsBusy = false;
            }
        }
        async void OnItemSelected(ReservationDto reservationDto)
        {
            await Shell.Current.Navigation.PushAsync(new DetailYourReservationPage(reservationDto.Id.Value));
        }


        async void OnSearchReservation()
        {
            await Shell.Current.Navigation.PushAsync(new SearchReservationPage());
        }


        public async Task OnAppearingAsync()
        {
            var isLoggedIn = Preferences.Get(AuthorizeConstants.IsLoggedInKey, string.Empty);

            if (isLoggedIn != "True")
            {
                //await Shell.SetTabBarIsVisible(this, false);
                await Shell.Current.Navigation.PushAsync(new LoginPage());
                //await Shell.Current.Navigation.PopToRootAsync();
            }
            IsBusy = true;
        }

        string GetApprovalStatus(bool approvalStatus, DateTime createdDate)
        {
            if (DateTime.Now > createdDate.AddMinutes(30) && approvalStatus == false)
            {
                return "หมดเวลาการยืนยัน";
            }

            else if (approvalStatus)
            {
                return "อนุมัติแล้ว";
            }
            else
            {
                return "ยังไม่อนุมัติ";
            }
        }
    }
}