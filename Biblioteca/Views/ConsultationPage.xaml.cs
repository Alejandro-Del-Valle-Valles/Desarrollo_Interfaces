using Biblioteca.Model;
using Biblioteca.Repositories;

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

    private string? _selectedFilterValue;
    public ConsultationPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Checks what options is pressed and show the authors or publisher based on the election.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAuthorsOrPublishersChecked(object? sender, CheckedChangedEventArgs e)
    {
        if (!e.Value) return;
        //TODO: Corregir esto
        if (rdbAuthor.IsChecked)
        {
            var authorNames = BooksRepository.books
                .GroupBy(b => b.Author)
                .Select(g => g.Key);

            GenerateDynamicLabels(vslAuthorsAndPublishers, authorNames);
        }
        else
        {
            var publisherNames = BooksRepository.books
                .GroupBy(b => b.Publisher)
                .Select(g => g.Key);

            GenerateDynamicLabels(vslAuthorsAndPublishers, publisherNames);
        }
    }

    private void LoadTitlesForFilter()
    {
        if (string.IsNullOrEmpty(_selectedFilterValue))
        {
            vslTitles.Clear();
            return;
        }

        IEnumerable<string> titlesToShow;

        if (rdbAuthor.IsChecked)
        {
            titlesToShow = BooksRepository.books
                .Where(b => b.Author == _selectedFilterValue)
                .Select(b => b.Title);
        }
        else
        {
            titlesToShow = BooksRepository.books
                .Where(b => b.Publisher == _selectedFilterValue)
                .Select(b => b.Title);
        }

        GenerateDynamicLabels(vslTitles, titlesToShow, OnBookTitleTapped);
    }

    /// <summary>
    /// Helper method to create dynamic, clickable labels in a specific layout.
    /// </summary>
    /// <param name="layout">The VerticalStackLayout to add labels to.</param>
    /// <param name="items">A list of strings to display as labels.</param>
    /// <param name="tapHandler">The event handler for the tap gesture.</param>
    private void GenerateDynamicLabels(VerticalStackLayout layout, IEnumerable<string> items, EventHandler<TappedEventArgs> tapHandler)
    {
        layout.Clear();

        foreach (var item in items)
        {
            Label newLabel = new Label
            {
                Text = item,
                TextColor = Colors.Black,
                Padding = new Thickness(5, 10)
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += tapHandler;

            newLabel.GestureRecognizers.Add(tap);

            layout.Add(newLabel);
        }
    }

    /// <summary>
    /// Handles the tap event on a BOOK TITLE label.
    /// </summary>
    private void OnBookTitleTapped(object sender, TappedEventArgs e)
    {
        if (sender is Label selectedTitleLabel)
        {
            string selectedTitle = selectedTitleLabel.Text;
        }
    }

    /// <summary>
    /// Handles the tap event on a dynamically generated label.
    /// Stores the selected value and provides visual feedback.
    /// </summary>
    /// <param name="sender">The object that was tapped (the Label).</param>
    /// <param name="e">Event arguments.</param>
    private void OnLabelTapped(object sender, TappedEventArgs e)
    {
        if (sender is Label selectedLabel)
        {
            _selectedFilterValue = selectedLabel.Text;
            foreach (var child in vslAuthorsAndPublishers.Children)
            {
                if (child is Label label)
                {
                    label.FontAttributes = FontAttributes.None;
                    label.BackgroundColor = Colors.Transparent;
                    label.TextColor = Colors.Black;
                }
            }

            selectedLabel.FontAttributes = FontAttributes.Bold;
            selectedLabel.BackgroundColor = Colors.LightGray;
            selectedLabel.TextColor = Colors.DarkBlue;

            LoadTitlesForFilter();
        }
    }
}