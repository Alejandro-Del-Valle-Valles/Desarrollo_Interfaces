using Tareas.Model;
using Tareas.Service;

namespace Tareas.Views;

public partial class CreateTaskPage : ContentPage
{
	public CreateTaskPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// When Save is clicked, check that the task has a Title and save it into the repository if it's correct.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSaveClicked(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            string title = TitleEntry.Text.Trim();
            Result result = RepositoryService.SaveTask(new(
                title,
                CommentEditor.Text.Trim(),
                DeliveryDate.Date
                ));
            if (result.IsSuccess)
            {
                DisplayAlert("Tarea Guardada con Éxito", $"La tarea {title} fue creada con éxito.", "Ok");
                ClearData();
            }
            else DisplayAlert("No se puedo crear la tarea", 
                $"ha ocurrido un error al tratar de crear la tarea: {result.Exception?.Message ?? "Error Desconocido"}", "Ok");
        }
        else DisplayAlert("Añade un título", "Debes añadir un título para crear una tarea.", "Ok");
    }

    /// <summary>
    /// Clear the data fields.
    /// </summary>
    private void ClearData()
    {
        TitleEntry.Text = string.Empty;
        CommentEditor.Text = string.Empty;
        DeliveryDate.Date = DateTime.Now;
    }
}