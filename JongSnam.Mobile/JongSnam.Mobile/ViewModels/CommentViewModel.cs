using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        private readonly IReviewServices _reviewService;

        private int _ratting;
        private string _message;
        private string _messageRatting;
        private ImageSource _imageStar5;
        private ImageSource _imageStar4;
        private ImageSource _imageStar3;
        private ImageSource _imageStar2;
        private ImageSource _imageStar1;
        public ImageSource star = ImageSource.FromFile("addedStar.png");
        public ImageSource unstar = ImageSource.FromFile("unAddedStar.png");

        public Command SaveCommand { get; }
        public Command AddStar1Command { get; }
        public Command AddStar2Command { get; }
        public Command AddStar3Command { get; }
        public Command AddStar4Command { get; }
        public Command AddStar5Command { get; }

        

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        public string MessageRatting
        {
            get => _messageRatting;
            set
            {
                _messageRatting = value;
                OnPropertyChanged(nameof(MessageRatting));
            }
        }

        public ImageSource ImageStar1
        {
            get => _imageStar1;
            set
            {
                _imageStar1 = value;
                OnPropertyChanged(nameof(ImageStar1));
            }
        }
        
        public ImageSource ImageStar2
        {
            get => _imageStar2;
            set
            {
                _imageStar2 = value;
                OnPropertyChanged(nameof(ImageStar2));
            }
        }
        
        public ImageSource ImageStar3
        {
            get => _imageStar3;
            set
            {
                _imageStar3 = value;
                OnPropertyChanged(nameof(ImageStar3));
            }
        }
        
        public ImageSource ImageStar4
        {
            get => _imageStar4;
            set
            {
                _imageStar4 = value;
                OnPropertyChanged(nameof(ImageStar4));
            }
        }
        
        public ImageSource ImageStar5
        {
            get => _imageStar5;
            set
            {
                _imageStar5 = value;
                OnPropertyChanged(nameof(ImageStar5));
            }
        }


        public CommentViewModel(int storeId)
        {
            _reviewService = DependencyService.Get<IReviewServices>();

            SaveCommand = new Command(async () => await OnSaveCommand(storeId));

            Task.Run(async () => await ExecuteLoadItemsCommand());

            AddStar1Command = new Command(OnAddStar1Command);
            AddStar2Command = new Command(OnAddStar2Command);
            AddStar3Command = new Command(OnAddStar3Command);
            AddStar4Command = new Command(OnAddStar4Command);
            AddStar5Command = new Command(OnAddStar5Command);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                ImageStar1 = unstar;
                ImageStar2 = unstar;
                ImageStar3 = unstar;
                ImageStar4 = unstar;
                ImageStar5 = unstar;
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


        private void OnAddStar1Command(object obj)
        {
            ImageStar1 = star;
            ImageStar2 = unstar;
            ImageStar3 = unstar;
            ImageStar4 = unstar;
            ImageStar5 = unstar;
            _ratting = 1;
            MessageRatting = "แย่";
        }
        private void OnAddStar2Command(object obj)
        {
            ImageStar1 = star;
            ImageStar2 = star;
            ImageStar3 = unstar;
            ImageStar4 = unstar;
            ImageStar5 = unstar;
            _ratting = 2;
            MessageRatting = "ไม่ถูกใจ";
        }
        private void OnAddStar3Command(object obj)
        {
            ImageStar1 = star;
            ImageStar2 = star;
            ImageStar3 = star;
            ImageStar4 = unstar;
            ImageStar5 = unstar;
            _ratting = 3;
            MessageRatting = "ดี";
        }
        private void OnAddStar4Command(object obj)
        {
            ImageStar1 = star;
            ImageStar2 = star;
            ImageStar3 = star;
            ImageStar4 = star;
            ImageStar5 = unstar;
            _ratting = 4;
            MessageRatting = "ถูกใจ";
        }
        private void OnAddStar5Command(object obj)
        {
            ImageStar1 = star;
            ImageStar2 = star;
            ImageStar3 = star;
            ImageStar4 = star;
            ImageStar5 = star;
            _ratting = 5;
            MessageRatting = "รัก";
        }

        async Task OnSaveCommand(int storeId)
        {
            try 
            {
                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, null);
                var request = new ReviewRequest
                {
                    StoreId = storeId,
                    UserId = Convert.ToInt32(userId),
                    Message = Message,
                    Rating = _ratting
                };
                var statusSaved = await _reviewService.AddReview(request);
                if (!statusSaved)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "เกิดช้อผิดพลาดบางอย่าง", "ตกลง");
                }
                await Shell.Current.Navigation.PopAsync();

            }
            catch(Exception ex)
            {
                
                await Shell.Current.DisplayAlert("แจ้งเตือน!", "เกิดช้อผิดพลาดบางอย่าง", "ตกลง");
                return;
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }

        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
