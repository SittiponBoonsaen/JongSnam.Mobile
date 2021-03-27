using JongSnam.Mobile.ViewModels;
using JongSnam.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace JongSnam.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(CommentPage), typeof(CommentPage));
            Routing.RegisterRoute(nameof(YearGraphPage), typeof(YearGraphPage));
            Routing.RegisterRoute(nameof(MonthGraphPage), typeof(MonthGraphPage));
            Routing.RegisterRoute(nameof(DayGraphPage), typeof(DayGraphPage));
            Routing.RegisterRoute(nameof(ResultSearchYourReservationPage), typeof(ResultSearchYourReservationPage));
            Routing.RegisterRoute(nameof(DetailYourReservationPage), typeof(DetailYourReservationPage));
            Routing.RegisterRoute(nameof(YourReservationPage), typeof(YourReservationPage));
            Routing.RegisterRoute(nameof(SearchReservationPage), typeof(SearchReservationPage));
            Routing.RegisterRoute(nameof(ResultSearchItemPage), typeof(ResultSearchItemPage));
            Routing.RegisterRoute(nameof(ReviewPage), typeof(ReviewPage));
            Routing.RegisterRoute(nameof(FieldPage), typeof(FieldPage));
            Routing.RegisterRoute(nameof(ListFieldPage), typeof(ListFieldPage));
            Routing.RegisterRoute(nameof(SearchItemPage), typeof(SearchItemPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(YourStorePage), typeof(YourStorePage));
            Routing.RegisterRoute(nameof(YourProFilePage), typeof(YourProFilePage));
            Routing.RegisterRoute(nameof(AddStorePage), typeof(AddStorePage));
            Routing.RegisterRoute(nameof(UpdateStorePage), typeof(UpdateStorePage));
            Routing.RegisterRoute(nameof(YourFieldPage), typeof(YourFieldPage));
            Routing.RegisterRoute(nameof(ChangePasswordPage), typeof(ChangePasswordPage));
            Routing.RegisterRoute(nameof(UpdateFieldPage), typeof(UpdateFieldPage));
            Routing.RegisterRoute(nameof(AddFieldPage), typeof(AddFieldPage));
        }

    }
}
