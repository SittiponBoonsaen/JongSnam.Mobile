using JongSnam.Mobile.Models;
using JongSnam.Mobile.Services;
using JongSnam.Mobile.Services.Implementations;
using JongSnam.Mobile.Services.Interfaces;
using JongSnam.Mobile.SqliteRepository;
using JongSnam.Mobile.SqliteRepository.Interfaces;
using Xamarin.Forms;

[assembly: ExportFont("fa-solid-900.ttf")]
[assembly: ExportFont("fa-regular-400.ttf", Alias = "FontAwesome")]
[assembly: ExportFont("materialdesignicons-webfont.ttf", Alias = "MyIcon")]
namespace JongSnam.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IRepository<UserModel>, Repository<UserModel>>();
            DependencyService.Register<IConfigurationService, ConfigurationService>();
            DependencyService.Register<IFieldServices, FieldServices>();
            DependencyService.Register<IPaymentServices, PaymentServices>();
            DependencyService.Register<IReservationServices, ReservationServices>();
            DependencyService.Register<IReviewServices, ReviewServices>();
            DependencyService.Register<IStoreServices, StoreServices>();
            DependencyService.Register<IUsersServices, UsersServices>();
            DependencyService.Register<IAddressServices, AddressServices>();
            DependencyService.Register<IEnumServices, EnumServices>();
            DependencyService.Register<IAuthenticationServices, AuthenticationServices>();
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
