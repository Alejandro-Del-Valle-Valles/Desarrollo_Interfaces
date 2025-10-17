namespace Biblioteca.Views;

/*
 La funcionalidad es la siguiente:
El usuario debe pulsar si quiere que se muestren los autores o lad editoriales, y en base a lo que seleccione,
debemos mostrarle en la columna de "Editoriales/Autores" las editoriales o los autores.
Cuando pulse una editorial o un autor, mostrarle los los libros de ese autor o de esa editorial en la columna de títulos.
Y cuando pulse un título, mostrar la portada.
 */
public partial class ConsultationPage : ContentPage
{
	public ConsultationPage()
	{
		InitializeComponent();
	}

	private static void onAuthorsOrPublishersClicked(object sender, EventArgs e)
	{

	}
}