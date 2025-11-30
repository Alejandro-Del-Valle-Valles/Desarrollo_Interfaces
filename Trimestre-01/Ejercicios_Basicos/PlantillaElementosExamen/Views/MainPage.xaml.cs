using System.Text.Json;

namespace PlantillaElementosExamen.Views //TODO: Importante cambiar esto si se añade a una nueva carpeta la main view. También en AppShell
{
    public partial class MainPage : ContentPage
    {
        //TODO: Esto se usa para poder hacer operaciones CRUD sobre ficheros en este caso con Json
        private static readonly string JsonDirectory = FileSystem.AppDataDirectory;
        private static readonly string JsonPath = Path.Combine(JsonDirectory, "registers.json");
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        //TODO: El del profe es este:
        private static string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.json");

        public MainPage()
        {
            InitializeComponent();
            MostrarDatosTabla();
        }

        private void OnToolBarButtonClicked(object? sender, EventArgs e)
        {
            //Código a ejecutar del tool bar
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            //Método para botones
            bool aceptado = await DisplayAlert("¿Quieres continuar?", "Desde continuar con la acción:", "Si", "No");
            DisplayAlert("Error", "Ocurrió un error.", "Ok");
        }

        private void OnRadioButtonChanged(object? sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var rb = sender as RadioButton;
            }
        }

        private void OnSwitchToggled(object? sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                //Código
            }
        }

        private void OnCheckBoxChecked(object? sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var cb = sender as CheckBox;
            }
        }

        //Debe ser llamado en el constructor
        private void MostrarDatosTabla()
        {
            clvCustomers.ItemsSource = null;  //new IEnumerable<Object>();
            //Para asiganr datos a la vista dinámica.
        }

        //Seleccionar una imagen abriendo el explorador de archivos
        private async void PhotoPicker()
        {
            // Open a windows to selecet a photo
            FileResult? selectedImage = await MediaPicker.Default.PickPhotoAsync();

            if (selectedImage != null)
            {
                //Código a ejecutar
            }
            else
            {
                await DisplayAlert("Error", "No se pudo seleccionar ninguna imagen.", "Aceptar");
            }
        }
    }
}
