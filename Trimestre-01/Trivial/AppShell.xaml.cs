using Trivial.Views;

namespace Trivial
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CapitalsPage), typeof(CapitalsPage));
            Routing.RegisterRoute(nameof(CountrysPage), typeof(CountrysPage));
        }
    }
}
