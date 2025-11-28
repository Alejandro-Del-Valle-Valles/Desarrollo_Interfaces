using NotasRapidas.Model;
using NotasRapidas.Repository;

namespace NotasRapidas.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the Save Button is clicked, chek that the note has params and save it into a Json.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSaveClicked(object? sender, EventArgs e)
        {
            if (CheckData())
            {
                Result result = NotesJsonRepository.Insert(new(TitleEntry.Text, ContentEditor.Text));
                if (result.IsSuccess)
                {
                    DisplayAlert("Nota Guardada", "La nota se ha guardado correctamente", "Ok");
                    ClearData();
                }
                else DisplayAlert("Error", $"No se pudo guardar la nota: {result.Exception?.Message ?? "Error Inesperado"}", "Ok");
            }
            else DisplayAlert("Rellena los campos", "Para crear una nota debes rellenar todos los campos.", "Ok");
        }

        private bool CheckData()
        {
            bool isCorrect = !string.IsNullOrWhiteSpace(TitleEntry.Text);
            if (string.IsNullOrWhiteSpace(ContentEditor.Text)) isCorrect = false;
            return isCorrect;
        }

        private void ClearData()
        {
            TitleEntry.Text = string.Empty;
            ContentEditor.Text = string.Empty;
        }
    }
}
