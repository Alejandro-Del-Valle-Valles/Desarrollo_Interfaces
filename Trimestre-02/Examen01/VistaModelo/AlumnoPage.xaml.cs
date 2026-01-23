using System.Collections.ObjectModel;
using Examen01.Entity;
using Examen01.enums;
using Examen01.Reposiotrio;

namespace Examen01.VistaModelo;

public partial class AlumnoPage : ContentPage
{

    public ObservableCollection<Alumno> Alumnos = new();

	public AlumnoPage()
	{
		InitializeComponent();
        pkBusqueda.ItemsSource = Enum.GetValues(typeof(TiposBusqueda));
        AlumnosColeccion.ItemsSource = Alumnos;
        InicializarAlumnos();
    }


    private void OnAlumnoChanged(object? sender, SelectionChangedEventArgs e)
    {
        
    }

    private void OnBuscarClicked(object? sender, EventArgs e)
    {
        if (sender != null)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {

            }
        }
    }

    private void OnGuardarClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnActualizarClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnEliminarClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnLimpiarClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private async void InicializarAlumnos()
    {
        try
        {
            await AlumnoRepository.IniciarBaseDatos();
            foreach (var alumno in await AlumnoRepository.GetAll())
            {
                Alumnos.Add(alumno);
            }
        }
        catch (Exception e)
        {
            await DisplayAlert("Error al cargar los datos", e.Message, "Aceptar");
        }
    }
}