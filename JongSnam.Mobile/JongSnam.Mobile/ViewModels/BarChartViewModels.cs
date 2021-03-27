using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace JongSnam.Mobile.ViewModels
{
    public class BarChartViewModels
    {
        public PlotModel Model { get; set; }
        public BarChartViewModels()
        {
            Model = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                LegendPadding = 200,

            };
            CategoryAxis xaxis = new CategoryAxis { Position = AxisPosition.Left };
            xaxis.MajorGridlineStyle = LineStyle.Solid;
            xaxis.MinorGridlineStyle = LineStyle.Dot;
            xaxis.Labels.Add("1");
            xaxis.Labels.Add("2");
            xaxis.Labels.Add("3");
            xaxis.Labels.Add("4");
            xaxis.Labels.Add("5");

            LinearAxis yaxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };
            yaxis.MajorGridlineStyle = LineStyle.Dot;
            yaxis.MinorGridlineStyle = LineStyle.Dot;

            var s1 = new BarSeries { };
            s1.Items.Add(new BarItem { Value = 25 });
            s1.Items.Add(new BarItem { Value = 15 });
            s1.Items.Add(new BarItem { Value = 10 });
            s1.Items.Add(new BarItem { Value = 40 });
            s1.Items.Add(new BarItem { Value = 25 });

            Model.Title = "Rating Store";
            Model.Background = OxyColor.Parse("#f5f5f3");

            Model.Series.Add(s1);


            Model.Axes.Add(xaxis);
            Model.Axes.Add(yaxis);


        }

        internal void OnAppearing()
        {

        }
    }
}
