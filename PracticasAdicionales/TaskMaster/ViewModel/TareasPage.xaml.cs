using Microsoft.Maui.Controls.Shapes;

namespace TaskMaster.ViewModel;

public partial class TareasPage : ContentPage
{
    private int numProyectos = 0;
	public TareasPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Muestra una pantalla de ayuda para crear proyectos
    /// </summary>
    /// <param name="sender">la fuente del evento</param>
    /// <param name="e">El dato del evento asociado a la pulsación</param>
    private async void OnHelpClicked(object? sender, TappedEventArgs e)
    {
        await DisplayAlert("¿Cómo crear un proyecto?", "Para para crear un proyecto debes pulsar el botón que indica 'Crear Proyecto' y rellenar los datos que indica.",
            "Aceptar");
    }

    /// <summary>
    /// Crea un nuevo proyecto
    /// </summary>
    /// <param name="sender">la fuente del evento</param>
    /// <param name="e">Datos del evento</param>
    private void OnCrearClicked(object? sender, EventArgs e)
    {
        numProyectos++;
        Border nuevoProyecto = new Border
        {
            Stroke = Colors.Navy,
            StrokeThickness = 2,
            Margin = 5,
            Padding = 5,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            BackgroundColor = Colors.Gray
        };

        VerticalStackLayout contenedor = new VerticalStackLayout
        {
            Padding = 5,
            Margin = 5
        };

        Label titulo = new Label
        {
            Text = $"Proyecto {numProyectos}",
            TextColor = Colors.Black,
            FontAttributes = FontAttributes.Bold,
            FontSize = 18f,
            Padding = 5,
            Margin = 5
        };

        Label descripcion = new Label
        {
            Text = $"Descripción del proyecto {numProyectos}",
            TextColor = Colors.Black,
            FontAttributes = FontAttributes.Bold,
            Padding = 5,
            Margin = 5
        };

        contenedor.Children.Add(titulo);
        contenedor.Children.Add(descripcion);

        nuevoProyecto.Content = contenedor;
        vlContenedor.Children.Add(nuevoProyecto);
    }
}