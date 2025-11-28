using NotasRapidas.Model;
using NotasRapidas.Repository;

namespace NotasRapidas.Views;

public partial class NotesPage : ContentPage
{
	public NotesPage()
	{
		InitializeComponent();
        ShowNotes();
	}

    private void ShowNotes()
    {
        Result<IEnumerable<Note>?> result = NotesJsonRepository.GetAll();
        if (result.IsSuccess)
        {
            IEnumerable<Note>? notes = result.Data;
            if (notes != null) NotesView.ItemsSource = notes;
            else DisplayAlert("Sin Notas", "No se han encontrado notas en el fichero.", "Ok");
        }
        else DisplayAlert("Error", $"Ha ocurrido un error: {result.Exception?.Message ?? "Error Inesperado"}", "Ok");
    }
}