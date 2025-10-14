using Biblioteca.Extensions;
using Biblioteca.Exceptions;

namespace Biblioteca.Model
{
    internal class Book : IEquatable<Book?>, IComparable<Book>
    {
        private string _title = "Desconocido";
        public string Title
        {
            get => _title;
            set
            {
                if(!string.IsNullOrEmpty(value.Trim())) _title = value.Trim().Capitalize();
            }
        }
        private string _author = "Desconocido";
        public string Author
        {
            get => _author;
            set
            {
                if (!string.IsNullOrEmpty(value.Trim())) _author = value.Trim().Capitalize();
            }
        }
        private string _publisher = "Desconocida";
        public string Publisher
        {
            get => _publisher;
            set
            {
                if(!string.IsNullOrEmpty(value.Trim())) _publisher = value.Trim().Capitalize();
            }
        }
        private string _imageUri = String.Empty;
        public string ImageURI
        {
            get => _imageUri;
            set
            {
                if (!string.IsNullOrEmpty(value.Trim())) _imageUri = value.Trim();
                else throw new NoImageException("Se debe añadir una imagen.");
            }
        }

        /// <summary>
        /// Needs a Title, Author, Publisher and URI of a Image.
        /// Capitalize the Title, Author and Publisher.
        /// If the Title, Author or Publisher is Empty, is seated to "Desconocido".
        /// If ImageURI is empty, throws a NoImageException.
        /// </summary>
        /// <exception cref="NoImageException">Throwed if the URI is empty</exception>
        /// <param name="title">string Title of the book</param>
        /// <param name="author">string Author of the book</param>
        /// <param name="publisher">string Publisher of the book</param>
        /// <param name="imageURI">string URI of the Image to show</param>
        public Book(string title, string author, string publisher, string imageURI)
        {
            Title = title;
            Author = author;
            Publisher = publisher;
            ImageURI = imageURI;
        }

        /// <summary>
        /// Two Books are equal if they have the same Title, Author and Publisher
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>bool, True if ther are the same, False otherwise</returns>
        public override bool Equals(object? obj) => Equals(obj as Book);

        /// <summary>
        /// Two Books are equal if they have the same Title, Author and Publisher
        /// </summary>
        /// <param name="other">Book to compare</param>
        /// <returns>bool, True if ther are the same, False otherwise</returns>
        public bool Equals(Book? other) => other is not null &&
                   Title == other.Title &&
                   Author == other.Author &&
                   Publisher == other.Publisher;

        public override int GetHashCode() => HashCode.Combine(Title, Author, Publisher);

        public static bool operator ==(Book? left, Book? right) => EqualityComparer<Book>.Default.Equals(left, right);

        public static bool operator !=(Book? left, Book? right) => !(left == right);

        /// <summary>
        /// Compare first by their Title, then by their Author and then by their Publisher.
        /// </summary>
        /// <param name="other">Book to compare</param>
        /// <returns>int comparation</returns>
        public int CompareTo(Book? other)
        {
            int result = string.Compare(Title, other?.Title, StringComparison.OrdinalIgnoreCase);
            if (result == 0) result = string.Compare(Author, other?.Author, StringComparison.OrdinalIgnoreCase);
            if (result == 0) result = string.Compare(Publisher, other?.Publisher, StringComparison.OrdinalIgnoreCase);
            return result;
        }
    }
}
