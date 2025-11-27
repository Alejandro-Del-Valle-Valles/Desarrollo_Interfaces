using Tareas.Model;
using Tareas.Service;

namespace Tareas.Views;

public partial class ListTaskPage : ContentPage
{
	public ListTaskPage()
	{
		InitializeComponent();
        ShowTasks();
	}

    private void ShowTasks()
    {
        Result<IEnumerable<Exercise>?> result = RepositoryService.GetAllTask();
        if (result.IsSuccess)
        {
            if (result.Object != null) TasksView.ItemsSource = result.Object;
            else DisplayAlert("Sin Tareas", "No se han encontrado tareas.", "Ok");
        }
        else DisplayAlert("Error", $"Ha ocurrido un error al tratar de obtener las tareas: {result.Exception?.Message ?? "Desconocido"}", "Ok");
    }
}