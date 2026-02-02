using System.Collections.ObjectModel;
using SimulacroExamen.Entity;
using SimulacroExamen.Repository;

namespace SimulacroExamen.VistaModelo;

public partial class AdministracionPage : ContentPage
{
    private ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
    private Producto? productoSeleccionado = null;
	public AdministracionPage()
	{
		InitializeComponent();
        try
        {
            foreach (var producto in ProductoRepository.GetAll().Result)
            {
                productos.Add(producto);
            }

            ProductosColeccion.ItemsSource = productos;
        }
        catch
        {
            DisplayAlert("Error al cargar los datos de la BBDD", "Ha ocurrido un error al cargar los datos de la BBDD",
                "Aceptar");
        }
	}

    private void OnProductoChanged(object? sender, SelectionChangedEventArgs e)
    {
        productoSeleccionado = e.CurrentSelection.FirstOrDefault() as Producto;
        if (productoSeleccionado != null)
        {
            EtNombre.Text = productoSeleccionado.Nombre;
            EtDescripcion.Text = productoSeleccionado.Descripcion;
            EtPrecio.Text = productoSeleccionado.Precio.ToString();
        }
    }

    private void OnPrecioTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
            return;

        bool isValid = float.TryParse(e.NewTextValue, out _);

        if (!isValid)
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }
    private string NormalizarPrecio(Entry entry) => entry.Text?.Replace('.', ',') ?? "0";

    private async void OnActualizarClicked(object? sender, EventArgs e)
    {
        if (productoSeleccionado != null && ComprobarDatos())
        {
            try
            {
                productoSeleccionado.Descripcion = EtDescripcion.Text.Trim();
                productoSeleccionado.Precio = float.Parse(NormalizarPrecio(EtPrecio));
                if (await ProductoRepository.Update(productoSeleccionado))
                {
                    await DisplayAlert("Producto actualizado con éxito", "El producto se actualizó con éxito",
                        "Aceptar");
                    productos[productos.IndexOf(productoSeleccionado)] = productoSeleccionado;
                }
                else await DisplayAlert("Producto no actualizado", "El producto no se pudo actualizar", "Aceptar");
            }
            catch
            {
                await DisplayAlert("Error al actualizar el producto",
                    "El producto no se pudo actualizar debido a un error.", "Aceptar");
            }
        }
        else
            await DisplayAlert("Datos No Válidos",
                "Debes seleccionar un producto e introducir una descripción y un precio superior a 0", "Aceptar");
    }

    private async void OnEliminarClicked(object? sender, EventArgs e)
    {
        if (productoSeleccionado != null)
        {
            if (await DisplayAlert("Confirmar eliminación",
                    "La eliminación es permannete por lo que el producto desaparecerá para siempre.",
                    "Confirmar Eliminación",
                    "Cancelar"))
            {
                try
                {
                    if (await ProductoRepository.Delete(productoSeleccionado.Nombre))
                    {
                        productos.Remove(productoSeleccionado);
                        EtNombre.Text = String.Empty;
                        EtDescripcion.Text = String.Empty;
                        EtPrecio.Text = String.Empty;
                        await DisplayAlert("Producto eliminado correctamente", "El producto se ha eliminado con éxito",
                            "Aceptar");
                        productoSeleccionado = null;
                    }
                    else
                        await DisplayAlert("No se ha podido eliminar el producto",
                            "El producto no se ha eliminado. Es posible que ya no exista.", "Aceptar");
                }
                catch
                {
                    await DisplayAlert("No se ha podido eliminar el producto",
                        "Ha ocurrido un error y no se ha podido eliminar el producto.", "Aceptar");
                }
            }
        }
        else
            await DisplayAlert("Debes seleccionar un producto", "Para eliminar un porducto debes seleccionarlo antes",
                "Aceptar");
    }

    private bool ComprobarDatos()
    {
        bool descripcionCorrecta = !string.IsNullOrEmpty(EtDescripcion.Text);
        bool precioCorrecto = !string.IsNullOrEmpty(EtDescripcion.Text) && float.Parse(NormalizarPrecio(EtPrecio)) > 0;
        return descripcionCorrecta && precioCorrecto;
    }
}