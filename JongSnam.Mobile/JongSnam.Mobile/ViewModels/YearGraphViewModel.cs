using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public class YearGraphViewModel : BaseViewModel
    {
        private readonly IReservationServices _reservationServices;
        public PlotModel Model { get; set; }

        public YearGraphViewModel(ObservableCollection<ReservationDto> items)
        {
            _reservationServices = DependencyService.Get<IReservationServices>();

            Model = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                LegendPadding = 200,

            };

            Task.Run(async () => await ExecuteLoadItemsCommand());


        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                Model.InvalidatePlot(true);

                var linearAxis1 = new LinearAxis();
                linearAxis1.AbsoluteMinimum = 0;
                linearAxis1.AbsoluteMaximum = 200;

                var barSeries = new ColumnSeries
                {
                    LabelPlacement = LabelPlacement.Inside,
                    LabelFormatString = "{0}",

                };

                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, null);

                var data = await _reservationServices.GraphMonthReservation(Convert.ToInt32(userId), 3, 1, 100);

                int[] CountArrays = new int[13];

                foreach (var item in data)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        if (i == item.Months.Value)
                        {
                            CountArrays[item.Months.Value] = CountArrays[item.Months.Value] + 1;
                        }
                        else
                        {
                            CountArrays[item.Months.Value] = CountArrays[item.Months.Value] + 0;
                        }
                    }
                }

                String[] strNames = new String[] { "ม.ค", "ก.พ", "มี.ค", "เม.ย", "พ.ค", "มิ.ย",
                "ก.ค", "ส.ค", "ก.ย", "ต.ค", "พ.ย", "ธ.ค" };

                for (int i = 2; i <= 13; i++)
                {
                    barSeries.Items.Add(new ColumnItem
                    {
                        Value = Convert.ToDouble(CountArrays[i - 1]),
                        Color = OxyColor.Parse("#f1c40f")
                    });
                }
                Model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Bottom,
                    Key = "Sample Data",
                    ItemsSource = strNames,
                    IsPanEnabled = false,
                    IsZoomEnabled = false,
                    Selectable = false,
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
