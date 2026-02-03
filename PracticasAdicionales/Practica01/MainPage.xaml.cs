using Practica01.Core.Service;

namespace Practica01
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            InicializarDatos();
        }

        private void InicializarDatos()
        {
            
             VentaService.Insert(new("Enero", 276.5m));
           VentaService.Insert(new("Febrero", 450.4m));
           VentaService.Insert(new("Marzo", 340.56m));
           VentaService.Insert(new("Abril", 300m));
           VentaService.Insert(new("Mayo", 370.12m));
           VentaService.Insert(new("Junio", 401.2m));
            
        }
    }
}
