using JongSnam.Mobile.Services;
using JongSnam.Mobile.Services.Implementations;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JongSnam.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IConfigurationService, ConfigurationService>();
            DependencyService.Register<IUsersServices, UsersServices>();
            DependencyService.Register<IStoreServices, StoreServices>();

            DependencyService.Register<MockDataStore>();

            MainPage = new AppShell();



        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
