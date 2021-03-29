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
    public class MonthGraphViewModel : BaseViewModel
    {
        public PlotModel Model { get; set; }

        public MonthGraphViewModel(ObservableCollection<ReservationDto> items)
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

                //barSeries.Items.Add(new ColumnItem
                //{
                //    Color = OxyColor.Parse("#3498db")
                //});

                //barSeries.Items.Add(new ColumnItem
                //{
                //    Color = OxyColor.Parse("#2ecc71")
                //});
                //barSeries.Items.Add(new ColumnItem
                //{
                //    Color = OxyColor.Parse("#9b59b6")
                //});

                //barSeries.Items.Add(new ColumnItem
                //{
                //    Color = OxyColor.Parse("#34495e")
                //});

                //barSeries.Items.Add(new ColumnItem
                //{
                //    Color = OxyColor.Parse("#e74c3c")
                //});

                //barSeries.Items.Add(new ColumnItem
                //{
                //    Color = OxyColor.FromRgb
                //});

                string[] n = new string[31];
                Random rnd = new Random();
                for (int i = 0; i < n.Length; i++)
                {
                    int random = rnd.Next(1, 51);

                    byte r = (byte)rnd.Next(256);
                    byte g = (byte)rnd.Next(256);
                    byte b = (byte)rnd.Next(256);

                    barSeries.Items.Add(new ColumnItem
                    {
                        Value = Convert.ToDouble(random),
                        Color = OxyColor.FromRgb(r, g, b),
                    });
                }
                barSeries.IsStacked = false;
                Model.Series.Add(barSeries);
              String[] strNames = new String[] { "1", "2", "3", "4", "5", "6",
                "7", "8", "9", "10", "11", "12","13", "14", "15", "16", "17", "18",
                "19", "20", "21", "22", "23", "24","25", "26", "27", "28", "29", "30",
                "31"};

                Model.Axes.Add(new CategoryAxis
                {
                    Position = AxisPosition.Bottom,
                    Key = "Sample Data",
                    ItemsSource = strNames,
                    IsZoomEnabled = false,
                    IsPanEnabled = true,
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
