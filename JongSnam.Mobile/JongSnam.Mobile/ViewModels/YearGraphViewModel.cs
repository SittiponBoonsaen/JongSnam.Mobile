using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using JongSnam.Mobile.Constants;
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

                var linearAxis1 = new LinearAxis();
                linearAxis1.AbsoluteMinimum = 0;
                linearAxis1.AbsoluteMaximum = 200;


                var barSeries = new ColumnSeries
                {
                    LabelPlacement = LabelPlacement.Inside,
                    LabelFormatString = "{0}",


                };

                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, null);
                //var data = await _reservationServices.


                barSeries.Items.Add(new ColumnItem
                {
                    Value = Convert.ToDouble(33),
                    Color = OxyColor.Parse("#3498db")
                });

                barSeries.Items.Add(new ColumnItem
                {
                    Value = Convert.ToDouble(196),
                    Color = OxyColor.Parse("#2ecc71")
                });


                barSeries.Items.Add(new ColumnItem
                {
                    Value = Convert.ToDouble(152),
                    Color = OxyColor.Parse("#9b59b6")
                });

                barSeries.Items.Add(new ColumnItem
                {
                    Value = Convert.ToDouble(62),
                    Color = OxyColor.Parse("#34495e")
                });

                barSeries.Items.Add(new ColumnItem
                {
                    Value = Convert.ToDouble(68),
                    Color = OxyColor.Parse("#e74c3c")
                });

                barSeries.Items.Add(new ColumnItem
                {
                    Value = Convert.ToDouble(101),
                    Color = OxyColor.Parse("#f1c40f")
                });
                Model.Series.Add(barSeries);
                String[] strNames = new String[] { "ม.ค", "ก.พ", "มี.ค", "เม.ย", "พ.ค", "มิ.ย",
                "ก.ค", "ส.ค", "ก.ย", "ต.ค", "พ.ย", "ธ.ค" };
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
    }
}
