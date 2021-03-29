using System;
using System.Collections.Generic;
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

        private IsOpen _selectYear;

        public PlotModel Model { get; set; }

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
            get => _selectMonth;
            set
            {
                _selectMonth = value;
                OnPropertyChanged(nameof(selectMonth));
            }
        }

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

                var data = await _reservationServices.GraphMonthReservation(Convert.ToInt32(userId), selectMonth.Month, 1, 100);

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
