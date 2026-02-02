using SimulacroExamen.Repository;
using System.Linq;

namespace SimulacroExamen.VistaModelo;

public partial class CreacionPage : ContentPage
{
    private int numeroProducto = 0;
	public CreacionPage()
	{
		InitializeComponent();
	}

    private void OnAgregarClicked(object? sender, EventArgs e)
    {
        numeroProducto++;
        VerticalStackLayout contenedor = new VerticalStackLayout
        {
            Margin = new Thickness(5)
        };

        Entry nombre = new Entry
        {
            Placeholder = $"Nombre del producto {numeroProducto}",
            BackgroundColor = Colors.AliceBlue,
            TextColor = Colors.Black,
            Margin = new Thickness(5)
        };

        Entry descripcion = new Entry
        {
            Placeholder = $"Descripció del producto {numeroProducto}",
            BackgroundColor = Colors.AliceBlue,
            TextColor = Colors.Black,
            Margin = new Thickness(5)
        };

        Entry precio = new Entry
        {
            Placeholder = $"Precio del producto {numeroProducto}",
            BackgroundColor = Colors.AliceBlue,
            TextColor = Colors.Black,
            Margin = new Thickness(5)
        };

        precio.TextChanged += OnPrecioTextChanged;

        contenedor.Children.Add(nombre);
        contenedor.Children.Add(descripcion);
        contenedor.Children.Add(precio);
        VlProductos.Children.Add(contenedor);
    }

    private void OnEliminarClicked(object? sender, EventArgs e)
    {
        if (numeroProducto > 0)
        {
            numeroProducto--;
            VlProductos.Children.RemoveAt(numeroProducto);
        }
    }

    private async void OnGuardarClicked(object? sender, EventArgs e)
    {
        if (numeroProducto > 0)
        {
            IList<bool> insertados = new List<bool>();
            foreach (var hijo in VlProductos.Children)
            {
                var contenedor = hijo as VerticalStackLayout;
                var etNombre = contenedor?.Children.ElementAt(0) as Entry;
                var etDescripcion = contenedor?.Children.ElementAt(1) as Entry;
                var etPrecio = contenedor?.Children.ElementAt(2) as Entry;
                if (ValidarCampos(etNombre, etDescripcion, etPrecio))
                {
                    //Ya nunca van a estar vacíos
                    try
                    {
                        if (await ProductoRepository.GetByNombre(etNombre.Text) != null)
                        {
                            await DisplayAlert($"{etNombre.Text} ya existe.",
                                "No se ha guardado el producto porque ya existe uno con el mismo nombre", "Aceptar");
                            continue;
                        }

                        insertados.Add(await ProductoRepository.Insert(new(etNombre.Text.Trim(), etDescripcion.Text.Trim(), float.Parse(NormalizarPrecio(etPrecio)))));
                    }
                    catch (Exception exception)
                    {
                        await DisplayAlert($"Error al insertar {etNombre}", $"{etNombre} es posible que ya exista.",
                            "Aceptar");
                    }
                }
                else await DisplayAlert("Campos incorrectos", "Comprueba que todos los campos están rellenados y que el precio no es negativo o 0", "Aceptar");
            }

            if (insertados.Count > 0 && insertados.All(i => i))
                await DisplayAlert("Los productos se han guarado",
                    "Los productos se han guardad correctamente", "Aceptar");
            else
            {
                await DisplayAlert("Productos insertados", $"{insertados.Count(i => !i)} productos no se han insertado.", "Aceptar");
            }
        }
    }

    private bool ValidarCampos(Entry? nombre, Entry? descripcion, Entry? precio)
    {
        bool nombreValido = !string.IsNullOrEmpty(nombre?.Text);
        bool descripcionValida = !string.IsNullOrEmpty(descripcion?.Text);
        bool precioValido = !string.IsNullOrEmpty(precio?.Text) && float.Parse(NormalizarPrecio(precio)) > 0;
        return nombreValido && descripcionValida && precioValido;
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
}