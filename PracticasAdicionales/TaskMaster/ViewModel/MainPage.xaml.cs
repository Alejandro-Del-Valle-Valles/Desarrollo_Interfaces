namespace TaskMaster.ViewModel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Comprueba si los datos introducidos no están vacíos. Lanza una alerta por cada dato vacío. Si los datos están completos, avisa de que se inicó sesión.
        /// </summary>
        /// <param name="sender">la fuente del evento</param>
        /// <param name="e">Datos del evento/param>
        private async void OnLoginClicked(object? sender, EventArgs e)
        {
            bool emailEmpty = string.IsNullOrWhiteSpace(etMail.Text);
            bool passwordEmpty = string.IsNullOrWhiteSpace(etPassword.Text);
            if (emailEmpty) 
                await DisplayAlert("Introduce un Email", "Debes de introducir un email para iniciar sesión", "Ok");
            if (passwordEmpty)
                await DisplayAlert("Introduce una contraseña", "Debes introducir una contraseña para poder iniciar sesión", "Ok");
            if(!emailEmpty && !passwordEmpty) await DisplayAlert("Sesión Iniciada", "Has iniciado sesión correctamente", "Ok");
        }

        /// <summary>
        /// Comprueba si los datos introducidos no están vacíos. Lanza una alerta por cada dato vacío. Si los datos están completos, avisa de que se creó la cuenta.
        /// </summary>
        /// <param name="sender">la fuente del evento</param>
        /// <param name="e">Datos del evento/param>
        private async void OnCrearClicked(object? sender, EventArgs e)
        {
            bool emailEmpty = string.IsNullOrWhiteSpace(etMail.Text);
            bool passwordEmpty = string.IsNullOrWhiteSpace(etPassword.Text);
            if (emailEmpty)
                await DisplayAlert("Introduce un Email", "Debes de introducir un email para crear una cuenta", "Ok");
            if (passwordEmpty)
                await DisplayAlert("Introduce una contraseña", "Debes introducir una contraseña para poder crear una cuenta", "Ok");
            if (!emailEmpty && !passwordEmpty) await DisplayAlert("Cuenta Creada", "Has creado la cuenta correctamente", "Ok");
        }

        /// <summary>
        /// Muestra una pantalla de ayuda para iniciar sesión.
        /// </summary>
        /// <param name="sender">la fuente del evento</param>
        /// <param name="e">El dato del evento asociado a la pulsación</param>
        private async void OnHelpClicked(object? sender, TappedEventArgs e)
        {
            await DisplayAlert("¿Cómo iniciar sesión y crear una cuenta?",
"""
Si ya tiene una cuenta creada, debe introducir el correo y la contraseña con la que creó la cuenta y pulse "Iniciar sesión"
Si no tiene una cuenta creada, introduzca un correo y una contraseña, y pulse "Crear Cuenta". Recuerde apuntar la contraseña para no perder el acceso.
""",
                "Aceptar");
        }
    }
}
