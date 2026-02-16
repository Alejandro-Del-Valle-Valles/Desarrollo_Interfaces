namespace TaskMaster.ViewModel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object? sender, EventArgs e)
        {
            await DisplayAlert("Sesión Iniciada", "Has iniciado sesión correctamente", "Aceptar");
        }
    }
}
