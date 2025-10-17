using Biblioteca.Extensions;
using Biblioteca.Model;
using Biblioteca.Repositories;

namespace Biblioteca.Views;

public partial class RegisterPage : ContentPage
{
    private static string _imgPath = string.Empty; //Global variable for the full path of the selected image.
	public RegisterPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Clears all the fields.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void onCleanClicked(object sender, EventArgs e)
    {
		etyPublisher.Text = string.Empty;
		etyAuthor.Text = string.Empty;
		etyTitle.Text = string.Empty;
        imgCover.Source = null;
        _imgPath = string.Empty;
    }

    /// <summary>
    /// Save on the repository the new book
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	private async void onSaveClicked(object sender, EventArgs e)
	{
        try
        {
            if (etyPublisher.Text.Trim().IsNotEmpty() &&
                etyAuthor.Text.Trim().IsNotEmpty() &&
                etyTitle.Text.Trim().IsNotEmpty() &&
                _imgPath.IsNotEmpty())
            {
                Book bookToSave = new(etyTitle.Text.Capitalize(), etyAuthor.Text.Capitalize(),
                    etyPublisher.Text.Capitalize(), _imgPath);
                if (!BooksRepository.books.Contains(bookToSave)) BooksRepository.books.Add(bookToSave); //Save the book if isn't in the repository
                else await DisplayAlert("El libro ya existe", "El libro ya existe en la BBDD.", "Aceptar");
                    onCleanClicked(sender, e); //Clear the fields
                await DisplayAlert("Libro Guardado", "Libro guardado correctamente.", "Aceptar");
            }
            else
            {
                throw new Exception();
            }
        }
        catch
        {
            await DisplayAlert("Campos Vacíos","Hay capos vacíos", "Aceptar");
        }
    }

    /// <summary>
    /// Open a windows to select an image for the book. And save the absoluthe path in a global variable.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void onSelectImageClicked(object sender, EventArgs e)
    {
        // Open a windows to selecet a photo
        FileResult selectedImage = await MediaPicker.Default.PickPhotoAsync();

        if (selectedImage != null)
        {
            _imgPath = selectedImage.FullPath; //Save the Absolute Path
            imgCover.Source = ImageSource.FromFile(_imgPath);
        }
        else
        {
            await DisplayAlert("Error", "No se pudo seleccionar ninguna imagen.", "Aceptar");
        }
    }
}