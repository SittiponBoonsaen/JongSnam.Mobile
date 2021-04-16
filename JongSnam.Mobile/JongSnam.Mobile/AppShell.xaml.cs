using JongSnam.Mobile.Views;
using Xamarin.Forms;

namespace JongSnam.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("reservation", typeof(YourReservationPage));
        }

    }
}
