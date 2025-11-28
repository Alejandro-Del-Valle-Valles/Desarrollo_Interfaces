namespace PlantillaElementosExamen.Views //TODO: Importante cambiar esto si se añade a una nueva carpeta la main view. También en AppShell
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            MostrarDatosTabla();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            //Método para botones
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
    }
}
