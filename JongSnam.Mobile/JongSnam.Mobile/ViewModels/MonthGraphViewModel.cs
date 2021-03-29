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

        public Command LoadGraphCommand { get; private set; }

        private IsOpen _selectMonth;

        public List<IsOpen> selectMonths { get; set; } = new List<IsOpen>()
        {
        new IsOpen(){Name = "แสดงปี เดือน มกราคม" , Month = 1},
        new IsOpen(){Name = "แสดงปี เดือน กุมภาพันธ์", Month = 2},
        new IsOpen(){Name = "แสดงปี เดือน มีนาคม", Month = 3},
        new IsOpen(){Name = "แสดงปี เดือน เมษายน", Month = 4},
        new IsOpen(){Name = "แสดงปี เดือน พฤษภาคม " , Month = 5},
        new IsOpen(){Name = "แสดงปี เดือน มิถุนายน", Month = 6},
        new IsOpen(){Name = "แสดงปี เดือน กรกฎาคม", Month = 7},
        new IsOpen(){Name = "แสดงปี เดือน สิงหาคม", Month = 8},
        new IsOpen(){Name = "แสดงปี เดือน กันยายน" , Month = 9},
        new IsOpen(){Name = "แสดงปี เดือน ตุลาคม", Month = 10},
        new IsOpen(){Name = "แสดงปี เดือน พฤศจิกายน", Month = 11},
        new IsOpen(){Name = "แสดงปี เดือน ธันวาคม", Month = 12}
        };
        public IsOpen selectMonth
        {
            get { return _selectMonth; }

            set
            {
                _selectMonth = value;
                OnPropertyChanged(nameof(selectMonth));
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

            LoadGraphCommand = new Command(async () => await OnLoadGraphCommand(selectMonth.Month));


        }


        async Task OnLoadGraphCommand(int? Month)
        {
            
            try
            {
                IsBusy = true;

                Model.InvalidatePlot(true);

                var linearAxis1 = new LinearAxis();
                linearAxis1.AbsoluteMinimum = 0;
                linearAxis1.AbsoluteMaximum = 50;


                var barSeries = new ColumnSeries
                {
                    LabelPlacement = LabelPlacement.Inside,
                    LabelFormatString = "{0}",

                };
                int? month = Month == 0 || Month == null ? 0 : Month;
                if (month == 0)
                {
                    month = 0;
                }


                var userId = Preferences.Get(AuthorizeConstants.UserIdKey, null);

                var data = await _reservationServices.GraphMonthReservation(Convert.ToInt32(userId), (int)month, 1, 100);
                if (data == null)
                {
                    await Shell.Current.DisplayAlert("แจ้งเตือน!", "ไม่มีช้อมูลที่คุณเลือก", "ตกลง");
                    await Shell.Current.Navigation.PopAsync();
                    return;
                }
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
