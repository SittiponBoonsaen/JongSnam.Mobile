using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services.Interfaces;
using JongSnamService.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JongSnam.Mobile.ViewModels
{
    public class MonthGraphViewModel : BaseViewModel
    {
        private readonly IReservationServices _reservationServices;

        public ObservableCollection<GrahpDto> Items { get; }

        public PlotModel Model { get; set; }

        private Random rnd = new Random();

        private IsOpen _selectYear;


        public List<IsOpen> selectYears { get; set; } = new List<IsOpen>()
        {
        new IsOpen(){Name = "แสดงปี คส.2020" , Year = 2020},
        new IsOpen(){Name = "แสดงปี คส.2021", Year = 2021},
        new IsOpen(){Name = "แสดงปี คส.2022", Year = 2022},
        new IsOpen(){Name = "แสดงปี คส.2023", Year = 2023}
        };
        public IsOpen selectYear
        {
            get => _selectYear;
            set
            {
                _selectYear = value;
                OnPropertyChanged(nameof(selectYear));
            }
        }

        public MonthGraphViewModel(ObservableCollection<ReservationDto> items)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();

            Model = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                LegendPadding = 10,

            };

            Task.Run(async () => await ExecuteLoadItemsCommand());


        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {

                var linearAxis1 = new LinearAxis();
                linearAxis1.AbsoluteMinimum = 0;
                linearAxis1.AbsoluteMaximum = 50;


                var barSeries = new ColumnSeries
                {
                    LabelPlacement = LabelPlacement.Inside,
                    LabelFormatString = "{0}",

                };

                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, null);

                var data = await _reservationServices.GraphYearReservation(Convert.ToInt32(userId), selectYear.Year, 1, 100);

                int[] CountArrays = new int[32];

                foreach (var item in data)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        if (i == item.Days.Value)
                        {
                            CountArrays[item.Days.Value] = CountArrays[item.Days.Value] + 1;
                        }
                        else
                        {
                            CountArrays[item.Days.Value] = CountArrays[item.Days.Value] + 0;
                        }
                    }
                }
                var strings = Enumerable.Range(1, 31)
                 .Select(i => i.ToString()).ToArray();
                for (int i = 2; i <= 32; i++)
                {
                    barSeries.Items.Add(new ColumnItem
                    {
                        Value = Convert.ToDouble(CountArrays[i - 1]),
                        Color = OxyColor.FromRgb((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256))
                    });
                }
                Model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Bottom,
                    Key = "Sample Data",
                    ItemsSource = strings,
                    IsPanEnabled = false,
                    IsZoomEnabled = false,
                    Selectable = false,
                    IntervalLength = 300,
                });
                Model.Axes.Add(linearAxis1);
                Model.Series.Add(barSeries);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                Model.InvalidatePlot(true);
            }
        }

        internal void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
