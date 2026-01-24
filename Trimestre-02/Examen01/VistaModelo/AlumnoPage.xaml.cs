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
        PkBusqueda.ItemsSource = Enum.GetValues(typeof(TiposBusqueda));
        AlumnosColeccion.ItemsSource = Alumnos;
        InicializarBD();
    }

    private async void InicializarBD()
    {
        try
        {
            await AlumnoRepository.IniciarBaseDatos();
            InicializarAlumnos();
        }
        catch (Exception e)
        {
            await DisplayAlert("Error al crear la BBDD", "Ha ocurrido un error al crear la BBDD.", "Aceptar");
        }
    }

    private async void InicializarAlumnos()
    {
        try
        {
            foreach (var alumno in await AlumnoRepository.GetAll())
            {
                Alumnos?.Add(alumno);
            }
        }
        catch (Exception e)
        {
            await DisplayAlert("Error al inicializar los alumnos", "Ha ocurrido un error al inicializar los alumnos",
                "Aceptar");
        }
    }

    private void OnAlumnoChanged(object? sender, SelectionChangedEventArgs e)
    {
        alumnoSeleccionado = e.CurrentSelection.FirstOrDefault() as Alumno;
        if (alumnoSeleccionado != null)
        {
            EtNombre.Text = alumnoSeleccionado.Nombre;
            EtNotaMedia.Text = alumnoSeleccionado.NotaMedia.ToString();
            DpFechaNacimiento.Date = DateTime.Parse(alumnoSeleccionado.FechaNacimiento);
        }
    }

    private async void OnBuscarClicked(object? sender, EventArgs e)
    {
        try
        {
            if (sender != null && !string.IsNullOrWhiteSpace(EtSearch.Text))
            {
                int indice = PkBusqueda.SelectedIndex;
                if (indice != -1)
                {
                    InicializarAlumnos();
                    switch ((TiposBusqueda)PkBusqueda.SelectedItem)
                    {
                        case TiposBusqueda.Id:
                            int id = int.Parse(EtSearch.Text);
                            Alumno? alumnoFiltrado = Alumnos?.FirstOrDefault(a => a.Id == id);
                            if (alumnoFiltrado != null)
                            {
                                Alumnos?.Clear();
                                Alumnos?.Add(alumnoFiltrado);
                            }
                            else await DisplayAlert("No se han encontrado datos", $"No se ha encontrado ningún alumno con ID {id}", "Aceptar");
                                break;
                        case TiposBusqueda.Nombre:
                            IList<Alumno>? alumnosNombre =
                                Alumnos?.Where(a => a.Nombre.Contains(EtSearch.Text.Trim())).ToList();
                            if (alumnosNombre != null)
                            {
                                Alumnos?.Clear();
                                foreach (Alumno alumno in alumnosNombre)
                                {
                                    Alumnos?.Add(alumno);
                                }
                            }
                            break;
                        case TiposBusqueda.Nota_Media:
                            float nota = float.Parse(EtSearch.Text);
                            IList<Alumno>? alumnosNota =
                                Alumnos?.Where(a => a.NotaMedia == nota).ToList();
                            if (alumnosNota != null)
                            {
                                Alumnos?.Clear();
                                foreach (Alumno alumno in alumnosNota)
                                {
                                    Alumnos?.Add(alumno);
                                }
                            }
                            break;
                        case TiposBusqueda.Fecha_Nacimiento:
                            IList<Alumno>? alumnosFecha =
                                Alumnos?.Where(a => a.FechaNacimiento.Contains(EtSearch.Text.Trim())).ToList();
                            if (alumnosFecha != null)
                            {
                                Alumnos?.Clear();
                                foreach (Alumno alumno in alumnosFecha)
                                {
                                    Alumnos?.Add(alumno);
                                }
                            }
                            break;
                    }
                }
            }
        }
        catch
        {
            await DisplayAlert("Error al buscar", "Ha ocurrido un error al tratar de buscar por los datos introducidos", "Aceptar");
        }
    }

    private async void OnGuardarClicked(object? sender, EventArgs e)
    {
        try
        {
            if (ComprobarDatos())
            {
                Alumno nuevoAlumno = new(Alumnos.LastOrDefault()?.Id + 1 ?? 1, EtNombre.Text.Trim(), 
                    float.Parse(EtNotaMedia.Text?.Replace('.', ',') ?? "0"), DpFechaNacimiento.Date.ToString("dd-MM-yyyy"));
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
                    alumnoSeleccionado.NotaMedia = float.Parse(NormalizarNota());
                    alumnoSeleccionado.FechaNacimiento = DpFechaNacimiento.Date.ToString("dd-MM-yyyy");
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
            Alumnos?.Clear();
            InicializarAlumnos();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error al limpiar los datos",
                "Ha ocurrido un error al limpiar los datos y cargar de nuevo los alumnos", "Aceptar");
        }
    }

    private void OnNotaMediaTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
            return;

        bool isValid = float.TryParse(e.NewTextValue, out _);

        if (!isValid)
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }

    private void OnBuscarTextChanged(object? sender, TextChangedEventArgs e)
    {
        TiposBusqueda filtro = (TiposBusqueda)PkBusqueda.SelectedItem;
        bool esValido;
        switch (filtro)
        {
            case TiposBusqueda.Id:
                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    return;

                esValido = int.TryParse(e.NewTextValue, out _);

                if (!esValido)
                    ((Entry)sender).Text = e.OldTextValue;
                break;
            case TiposBusqueda.Nota_Media:
                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    return;

                esValido = float.TryParse(e.NewTextValue, out _);
                if (!esValido) 
                    ((Entry)sender).Text = e.OldTextValue;
                break;
        }
    }

    private bool ComprobarDatos()
    {
        string notaMedia = NormalizarNota();
        bool nombreCorrecto = !String.IsNullOrWhiteSpace(EtNombre.Text);
        bool notaCorrecta = float.TryParse(notaMedia, out float media) && media is >= 0 and <= 10;
        bool fechCorrecta = DpFechaNacimiento.Date < DateTime.Today;
        if (!nombreCorrecto) DisplayAlert("El nombre no es válido", "El nombre no puede estar vacío.", "Aceptar");
        if (!notaCorrecta)
            DisplayAlert("La nota media no es válida", "La nota media debe estar entre 0 y 10", "Aceptar");
        if (!fechCorrecta) DisplayAlert("La fecha no es correcta", "La fecha debe ser inferior a hoy", "Aceptar");
        return nombreCorrecto && notaCorrecta && fechCorrecta;
    }

    private string NormalizarNota() => EtNotaMedia.Text?.Replace('.', ',') ?? "0";
}