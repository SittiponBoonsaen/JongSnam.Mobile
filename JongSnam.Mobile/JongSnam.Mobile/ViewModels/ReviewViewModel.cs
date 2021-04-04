using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using JongSnamService.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class ReviewViewModel : BaseViewModel
    {
        public PlotModel BarModel { get; set; }

        private readonly IReviewServices _reviewServices;
        private int _page = 1;
        private int _pageSize = 20;


        public ImageSource star { get; set; }
        public ImageSource unstar { get; set; }


        public ObservableCollection<ReviewDto> Items { get; }

        public Command LoadReview { get; }


        private double _ratingsum;
        private double _textComment;
        private bool _visible;
        private ImageSource _imageStar5;
        private ImageSource _imageStar4;
        private ImageSource _imageStar3;
        private ImageSource _imageStar2;
        private ImageSource _imageStar1;


        public double RatingSum
        {
            get => _ratingsum;
            set
            {
                _ratingsum = value;
                OnPropertyChanged(nameof(RatingSum));
            }
        }

        public double TextComment
        {
            get => _textComment;
            set
            {
                _textComment = value;
                OnPropertyChanged(nameof(TextComment));
            }
        }
        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }


        public ReviewViewModel(int storeId = 0)
        {
            BarModel = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                LegendPadding = 200,
                Title = "Rating Store",
                Background = OxyColor.Parse("#f5f5f3")
            };

            
            //BarModel = CreateBarChart(storeId).Result;

            _reviewServices = DependencyService.Get<IReviewServices>();

            Items = new ObservableCollection<ReviewDto>();

            Task.Run(async () => await ExecuteLoadItemsCommand(storeId));


            LoadReview = new Command(async () => await LoadReviews(storeId));

        }


        async Task LoadReviews(int storeId)
        {
            if (storeId == 0)
                return;
            IsBusy = true;
            try
            {
                star = ImageSource.FromFile("star.png");
                unstar = ImageSource.FromFile("unstar.png");
                Items.Clear();
                var items = await _reviewServices.GetReviewByStoreId(storeId, _page, _pageSize);
                foreach (var item in items.Collection)
                {

                    GetRatting(item.Rating.Value);

                    Items.Add(new ReviewDtoModel
                    { 
                        Message = item.Message,
                        Name = item.Name,
                        ImageStar1 = _imageStar1,
                        ImageStar2 = _imageStar2,
                        ImageStar3 = _imageStar3,
                        ImageStar4 = _imageStar4,
                        ImageStar5 = _imageStar5
                    });
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                BarModel.InvalidatePlot(true);
            }
        }

        async Task ExecuteLoadItemsCommand(int storeId)
        {
            if (storeId == 0)
                return;
            IsBusy = true;
            try
            {

                var userType = Preferences.Get(AuthorizeConstants.UserTypeKey, string.Empty);
                if (userType != "Owner")
                {
                    Visible = true;
                }

                var items = await _reviewServices.GetReviewByStoreId(storeId, _page, 100);

                var resultRating = (double)items.SummaryRating;

                RatingSum = Math.Round(resultRating, 1);

                CategoryAxis xaxis = new CategoryAxis
                {
                    Position = AxisPosition.Left,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.Dot
                };
                xaxis.Labels.Add("1");
                xaxis.Labels.Add("2");
                xaxis.Labels.Add("3");
                xaxis.Labels.Add("4");
                xaxis.Labels.Add("5");

                LinearAxis yaxis = new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MinimumPadding = 0,
                    MaximumPadding = 0.06,
                    AbsoluteMinimum = 0,
                    MajorGridlineStyle = LineStyle.Dot,
                    MinorGridlineStyle = LineStyle.Dot
                };

                var s1 = new BarSeries { };
                s1.Items.Add(new BarItem { Value = items.TotalOne ?? 0 });
                s1.Items.Add(new BarItem { Value = items.TotalTwo ?? 0 });
                s1.Items.Add(new BarItem { Value = items.TotalThree ?? 0 });
                s1.Items.Add(new BarItem { Value = items.TotalFour ?? 0 });
                s1.Items.Add(new BarItem { Value = items.TotalFive ?? 0 });

                BarModel.Series.Add(s1);
                BarModel.Axes.Add(xaxis);
                BarModel.Axes.Add(yaxis);
                BarModel.InvalidatePlot(true);
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

        internal void OnAppearing()
        {
            IsBusy = true;
        }
        public void GetRatting(double Rating)
        {

            if (Rating == 5)
            {
                _imageStar1 = star;
                _imageStar2 = star;
                _imageStar3 = star;
                _imageStar4 = star;
                _imageStar5 = star;
            }
            else if (Rating == 4)
            {
                _imageStar1 = star;
                _imageStar2 = star;
                _imageStar3 = star;
                _imageStar4 = star;
                _imageStar5 = unstar;

            }
            else if (Rating == 3)
            {
                _imageStar1 = star;
                _imageStar2 = star;
                _imageStar3 = star;
                _imageStar4 = unstar;
                _imageStar5 = unstar;

            }
            else if (Rating == 2)
            {
                _imageStar1 = star;
                _imageStar2 = star;
                _imageStar3 = unstar;
                _imageStar4 = unstar;
                _imageStar5 = unstar;

            }
            else if (Rating == 1)
            {
                _imageStar1 = star;
                _imageStar2 = unstar;
                _imageStar3 = unstar;
                _imageStar4 = unstar;
                _imageStar5 = unstar;
            }
        }
    }
}
