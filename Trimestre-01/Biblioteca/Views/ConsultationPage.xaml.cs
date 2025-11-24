using Biblioteca.Repositories;

namespace Biblioteca.Views;

public partial class ConsultationPage : ContentPage
{
    private string? _selectedFilterValue;

    public ConsultationPage()
    {
        InitializeComponent();
        var authorNames = BooksRepository.books
            .GroupBy(b => b.Author)
            .Select(g => g.Key);
        GenerateDynamicLabels(vslAuthorsAndPublishers, authorNames, OnLabelTapped);
    }

    /// <summary>
    /// Checks what options is pressed and show the authors or publisher based on the election.
    /// </summary>
    private void OnAuthorsOrPublishersChecked(object? sender, CheckedChangedEventArgs e)
    {
        if (vslTitles == null || imgCover == null) return;
        
        if (!e.Value) return;
        imgCover.Source = null;
        _selectedFilterValue = null;
        vslTitles.Clear();

        if (rdbAuthor.IsChecked)
        {
            var authorNames = BooksRepository.books
                .GroupBy(b => b.Author)
                .Select(g => g.Key);

            GenerateDynamicLabels(vslAuthorsAndPublishers, authorNames, OnLabelTapped);
        }
        else
        {
            var publisherNames = BooksRepository.books
                .GroupBy(b => b.Publisher)
                .Select(g => g.Key);

            GenerateDynamicLabels(vslAuthorsAndPublishers, publisherNames, OnLabelTapped);
        }
    }

    /// <summary>
    /// Show the titles by the filter selected by the user. (Author or Publisher)
    /// </summary>
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
            var selectedBook = BooksRepository.books
                .FirstOrDefault(b => b.Title == selectedTitle);

            if (selectedBook != null) imgCover.Source = ImageSource.FromFile(selectedBook.ImageURI);
            
        }
    }

    /// <summary>
    /// Handles the tap event on a dynamically generated label.
    /// Stores the selected value and provides visual feedback.
    /// </summary>
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
            imgCover.Source = null;

            LoadTitlesForFilter();
        }
    }
}