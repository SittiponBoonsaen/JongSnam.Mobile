using JongSnam.Mobile.Views;
using Xamarin.Forms;

namespace JongSnam.Mobile
{
    public partial class AppShellCustomer : Xamarin.Forms.Shell
    {
        public AppShellCustomer()
        {
            InitializeComponent();
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("reservation", typeof(YourReservationPage));
        }

    }
}
