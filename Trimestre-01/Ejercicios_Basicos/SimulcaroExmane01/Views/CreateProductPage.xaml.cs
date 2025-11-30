using SimulcaroExmane01.Repository;

namespace SimulcaroExmane01.Views;

public partial class CreateProductPage : ContentPage
{
	public CreateProductPage()
	{
		InitializeComponent();
	}

    private void OnGuardarClicked(object? sender, EventArgs e)
    {
        if (ComprobarDatos())
        {
            bool guardado = ProductosRepository.Guardar(
                new(NombreEntry.Text.Trim(), CategoriaEntry.Text.Trim(), (int)Math.Round(StockStepper.Value))
            );
            if (guardado)
            {
                DisplayAlert("Guardado", "El prodcuto se ha guardado con éxito.", "Ok");
                LimpiarCampos();
            }
            else DisplayAlert("Error", "Ha ocurrido un error y no se ha podido guardar el producto.", "Ok");
        }
        else DisplayAlert("Faltan Datos", "Faltan Datos para poder guardar el producto.", "Ok");
    }

    private bool ComprobarDatos()
    {
        bool esCorrecto = !string.IsNullOrWhiteSpace(NombreEntry.Text);
        if (string.IsNullOrWhiteSpace(CategoriaEntry.Text)) esCorrecto = false;
        return esCorrecto;
    }

    private void LimpiarCampos()
    {
        NombreEntry.Text = string.Empty;
        CategoriaEntry.Text = string.Empty;
        StockStepper.Value = 1;
    }
}