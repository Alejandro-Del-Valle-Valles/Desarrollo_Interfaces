using System.Collections.ObjectModel;
using Examen01.entity;
using Examen01.enums;
using Examen01.Reposiotrio;

namespace Examen01.VistaModelo;

public partial class AlumnoPage : ContentPage
{

    private ObservableCollection<Alumno>? Alumnos = new();
    private Alumno? alumnoSeleccionado = null;

	public AlumnoPage()
	{
		InitializeComponent();
        pkBusqueda.ItemsSource = Enum.GetValues(typeof(TiposBusqueda));
        AlumnosColeccion.ItemsSource = Alumnos;
        InicializarAlumnos();
    }

    private void OnAlumnoChanged(object? sender, SelectionChangedEventArgs e)
    {
        alumnoSeleccionado = e.CurrentSelection.FirstOrDefault() as Alumno;
        if (alumnoSeleccionado != null)
        {
            EtNombre.Text = alumnoSeleccionado.Nombre;
            EtNotaMedia.Text = alumnoSeleccionado.NotaMedia.ToString();
            DpFechaNacimiento.Date = alumnoSeleccionado.FechaNacimiento.ToDateTime(TimeOnly.MinValue);
        }
    }

    //TODO: Añadir las opciones de búsqueda
    private void OnBuscarClicked(object? sender, EventArgs e)
    {
        if (sender != null)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                switch ((TiposBusqueda)picker.SelectedItem)
                {
                    case TiposBusqueda.Id:
                        //TODO: Solo numeros enteros
                        break;
                    case TiposBusqueda.Nombre:
                        break;
                    case TiposBusqueda.Nota_Media:
                        //TODO: Solo permitir numeros decimales
                        break;
                    case TiposBusqueda.Fecha_Nacimiento:
                        break;
                }
            }
        }
    }

    private async void OnGuardarClicked(object? sender, EventArgs e)
    {
        try
        {
            if (ComprobarDatos())
            {
                Alumno nuevoAlumno = new(Alumnos.LastOrDefault()?.Id + 1 ?? 1, EtNombre.Text.Trim(), 
                    float.Parse(EtNotaMedia.Text?.Replace('.', ',') ?? "0"), DateOnly.FromDateTime(DpFechaNacimiento.Date));
                if (await AlumnoRepository.Insert(nuevoAlumno))
                {
                    Alumnos.Add(nuevoAlumno);
                    await DisplayAlert("Alumno guardado correctamente", "El alumno se ha guardado correctamente",
                        "Aceptar");
                }
                else
                    await DisplayAlert("Alumno NO guardado", "No se pudo guardar el alumno. Intentelo de nuevo.",
                        "Aceptar");
            }
        }
        catch (Exception exception)
        {
            await DisplayAlert("Error", "Ha ocurrido un error inesperado y el alumno no se ha podido guardar", "Aceptar");
        }
    }

    private async void OnActualizarClicked(object? sender, EventArgs e)
    {
        if (alumnoSeleccionado == null)
        {
            await DisplayAlert("Alumno no seleccionado", "Debes seleccionar un alumno para actualizarlo", "Aceptar");
            return;
        }
        try
        {
            if (ComprobarDatos())
            {
                if (await DisplayAlert("¿Estás seguro de que quieres actualizar al alumno?",
                        "Esta acción no es reversible.",
                        "Aceptar", "Cancelar"))
                {
                    alumnoSeleccionado.Nombre = EtNombre.Text.Trim();
                    alumnoSeleccionado.NotaMedia = float.Parse(EtNotaMedia.Text?.Replace('.', ',') ?? "0");
                    alumnoSeleccionado.FechaNacimiento = DateOnly.FromDateTime(DpFechaNacimiento.Date);
                    bool actualizado = await AlumnoRepository.Update(alumnoSeleccionado);
                    if (actualizado)
                    {
                        Alumnos[Alumnos.IndexOf(alumnoSeleccionado)] = alumnoSeleccionado;
                        await DisplayAlert("Alumno Actualizado Con Éxito",
                            "El alumno ha sido actualizado de forma exitosa",
                            "Aceptar");
                    }
                    else
                        await DisplayAlert("El alumno no fue eliminado",
                            "El alumno no se pudo eliminar. Es probable que no exista o ya haya sido eliminado.",
                            "Aceptar");
                }
            }
        }
        catch (Exception exception)
        {
            await DisplayAlert("Error al actualizar el alumno", "Ha ocurrido un error al actualizar el alumno", "Aceptar");
        }
    }

    private async void OnEliminarClicked(object? sender, EventArgs e)
    {
        if (alumnoSeleccionado == null)
        {
            await DisplayAlert("Alumno no seleccionado", "Debes seleccionar un alumno para eliminarlo", "Aceptar");
            return;
        }

        try
        {
            if (await DisplayAlert("¿Estás seguro de que quieres eliminar al alumno?", "Esta acción no es reversible.",
                    "Aceptar", "Cancelar"))
            {
                bool eliminado = await AlumnoRepository.Delete(alumnoSeleccionado.Id);
                if (eliminado)
                {
                    await DisplayAlert("Alumno Eliminado Con Éxito", "El alumno ha sido eliminado de forma exitosa",
                        "Aceptar");
                    Alumnos.Remove(alumnoSeleccionado);
                    EtNombre.Text = string.Empty;
                    EtNotaMedia.Text = string.Empty;
                    DpFechaNacimiento.Date = DateTime.Now;
                    alumnoSeleccionado = null;
                }
                else
                    await DisplayAlert("El alumno no fue eliminado",
                        "El alumno no se pudo eliminar. Es probable que no exista o ya haya sido eliminado.", "Aceptar");
            }
        }
        catch (Exception exception)
        {
            await DisplayAlert("Error al eliminar el alumno", "Ha ocurrido un error al eliminar el alumno", "Aceptar");
        }
    }

    private async void OnLimpiarClicked(object? sender, EventArgs e)
    {
        try
        {
            Alumnos.Clear();
            foreach (var alumno in await AlumnoRepository.GetAll())
            {
                Alumnos.Add(alumno);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error al limpiar los datos",
                "Ha ocurrido un error al limpiar los datos y cargar de nuevo los alumnos", "Aceptar");
        }
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

    private void OnNotaMediaTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
            return;

        bool isValid = double.TryParse(e.NewTextValue, out _);

        if (!isValid)
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }

    private bool ComprobarDatos()
    {
        string notaMedia = EtNotaMedia.Text?.Replace('.', ',') ?? "0";
        bool nombreCorrecto = !String.IsNullOrWhiteSpace(EtNombre.Text);
        bool notaCorrecta = float.TryParse(notaMedia, out float media) && media is >= 0 and <= 10;
        bool fechCorrecta = DpFechaNacimiento.Date < DateTime.Today;
        if (!nombreCorrecto) DisplayAlert("El nombre no es válido", "El nombre no puede estar vacío.", "Aceptar");
        if (!notaCorrecta)
            DisplayAlert("La nota media no es válida", "La nota media debe estar entre 0 y 10", "Aceptar");
        if (!fechCorrecta) DisplayAlert("La fecha no es correcta", "La fecha debe ser inferior a hoy", "Aceptar");
        return nombreCorrecto && notaCorrecta && fechCorrecta;
    }
}