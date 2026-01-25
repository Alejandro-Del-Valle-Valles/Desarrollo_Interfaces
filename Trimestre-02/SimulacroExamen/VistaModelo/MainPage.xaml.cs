using SimulacroExamen.Repository;

namespace SimulacroExamen.VistaModelo
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            try
            {
                ProductoRepository.InicializarBaseDatos().Wait();
            }
            catch
            {
                DisplayAlert("Error al crear la BBDD", "Ha ocurrido un error al crear la BBDD y no se podrá guardar datos.", "Aceptar");
            }
        }
    }
}
