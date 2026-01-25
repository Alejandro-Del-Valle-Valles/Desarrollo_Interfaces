namespace PruebasElementosDinamicos
{
    public partial class MainPage : ContentPage
    {
        private int contadorBoton = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAddClicked(object? sender, EventArgs e)
        {
            contadorBoton++;
            Button nuevoBoton = new Button
            {
                Text = $"Botón {contadorBoton}",
                BackgroundColor = Colors.Aqua,
                Margin = new Thickness(5)
            };
            nuevoBoton.Clicked += (s, args) =>
            {
                DisplayAlert("Botón Pulsado", $"Has pulsado el botón numero {contadorBoton}", "Aceptar");
            };
            VlContenedor.Children.Add(nuevoBoton);
        }

        private void OnDeleteClicked(object? sender, EventArgs e)
        {
            if (contadorBoton > 0)
            {
                contadorBoton--;
                VlContenedor.Children.RemoveAt(contadorBoton);
            }
            else DisplayAlert("No hay elementos que eliminar", "Tienes que agregar elementos para poder eliminarlos", "Aceptar");
        }
    }
}
