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
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(YourStorePage), typeof(YourStorePage));
            Routing.RegisterRoute(nameof(AddStorePage), typeof(AddStorePage));
            Routing.RegisterRoute(nameof(UpdateStorePage), typeof(UpdateStorePage));
            Routing.RegisterRoute(nameof(YourFieldPage), typeof(YourFieldPage));
        }

    }
}
