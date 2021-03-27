using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using JongSnamService.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace JongSnam.Mobile.ViewModels
{
    public class DayGraphViewModel : BaseViewModel
    {
        public PlotModel Model { get; set; }

        public DayGraphViewModel(ObservableCollection<ReservationDto> items)
        {
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
                String[] strNames = new String[] { "01", "02", "03", "04", "05", "06",
                "07", "08", "09", "10", "พ.ย", "ธ.ค" };
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
