using System.Collections.ObjectModel;
using System.Globalization;
using Practica01.Core.Entity;
using Practica01.Core.Helper;
using Practica01.Core.Service;

namespace Practica01
{
    public partial class MainPage : ContentPage
    {

        private ObservableCollection<Venta> ventas = new ObservableCollection<Venta>();

        public MainPage()
        {
            InitializeComponent();
            InicializarDatos();
            InicializarTabla();
            CalcularMetricas();
        }

        /// <summary>
        /// Inicializa los datos en la BBDD
        /// EN el caso de esta App solo lo hace en memoria
        /// </summary>
        private void InicializarDatos()
        { 
            VentaService.Insert(new("Enero", 276.5m)); 
            VentaService.Insert(new("Febrero", 450.4m)); 
            VentaService.Insert(new("Marzo", 340.56m)); 
            VentaService.Insert(new("Abril", 300m)); 
            VentaService.Insert(new("Mayo", 370.12m)); 
            VentaService.Insert(new("Junio", 401.2m));
        }

        /// <summary>
        /// Inicializa y muestra los datos de la tabla.
        /// </summary>
        private async void InicializarTabla()
        {
            try
            {
                Result<IList<Venta>?> result = VentaService.GetAll();
                if (result is { IsSuccess: true, Data: not null })
                {
                    result.Data.ToList().ForEach(ventas.Add);
                    VentasColeccion.ItemsSource = ventas;
                }
                else throw result.Exception ?? new Exception("No se han encontrado datos.");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error en la carga de datos", ex.Message, "Aceptar");
            }
        }

        private void CalcularMetricas()
        {
            var datos = VentaService.GetAll()?.Data?.ToList();
            var total = datos?.Sum(v => v.Total);
            LbTotal.Text = total?.ToString("C", CultureInfo.CurrentCulture) ?? 0.ToString("C", CultureInfo.CurrentCulture);

            var promedio = datos?.Average(v => v.Total);
            LbPromedio.Text = promedio?.ToString("C", CultureInfo.CurrentCulture) ?? 0.ToString("C", CultureInfo.CurrentCulture);

            var maximo = datos?.Max(v => v.Total);
            LbMaximo.Text = maximo?.ToString("C", CultureInfo.CurrentCulture) ?? 0.ToString("C", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Realiza la búsqueda de ventas por mes
        /// </summary>
        private async void OnBuscarClicked(object? sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(SbBusqueda.Text))
                {
                    var ventasMes = ventas.Where(v => v.Mes.ToLowerInvariant() == SbBusqueda.Text.ToLowerInvariant()).ToList();
                    if (ventasMes.Any())
                    {
                        ventas.Clear();
                        ventasMes.ForEach(ventas.Add);
                    }
                    else await DisplayAlert("Sin datos", $"No se han encontrado ventas que correspondan al mes {SbBusqueda.Text.Trim()}",
                        "Aceptar");
                }
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error en la carga de datos", ex.Message, "Aceptar");
            }
        }

        /// <summary>
        /// Muestra todas las ventas de nuevo cuando el texto está vacío
        /// </summary>
        private void OnBuscarChanged(object? sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SbBusqueda.Text))
                {
                    ventas.Clear();
                    Result<IList<Venta>?> result = VentaService.GetAll();
                    if (result is { IsSuccess: true, Data: not null })
                        result.Data.ToList().ForEach(ventas.Add);
                    
                }
            }
            catch {}
        }
    }
}
