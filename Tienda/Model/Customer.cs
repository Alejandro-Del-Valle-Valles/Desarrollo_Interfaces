using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tienda.Exceptions;
using Tienda.Extensions;

namespace Tienda.Model
{
    class Customer : IComparable<Customer>, INotifyPropertyChanged
    {
        private string _name = "Desconocido";
        private string _surname = "Desconocido";
        private string _city = "Desconocida";
        private string _email = "Desconocido";
        private string _comment = "Sin comentario";
        private bool _isVip = false;

        /// <summary>
        /// Getter and Setter for the Name.
        /// </summary>
        /// <exception cref="InvalidValueException">thrown if the name is empty or null.</exception>
        public string Name
        {
            get => _name;
            set
            {
                _name = string.IsNullOrEmpty(value.Trim())
                    ? throw new InvalidValueException("El nombre no puede estar vacío.")
                    : value.CapitalizeAll();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Getter and Setter for the Surname.
        /// </summary>
        /// <exception cref="InvalidValueException">thrown if the surname is empty or null.</exception>
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = string.IsNullOrEmpty(value.Trim())
                    ? throw new InvalidValueException("El apellido no puede estar vacío.")
                    : value.CapitalizeAll();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Getter and Setter for the City.
        /// </summary>
        /// <exception cref="InvalidValueException">thrown if the city is null or empty.</exception>
        public string City
        {
            get => _city;
            set
            {
                _city = string.IsNullOrEmpty(value.Trim())
                    ? throw new InvalidValueException("La ciudad no puede estar vacía.")
                    : value.Capitalize();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Getter and Setter for the Email.
        /// </summary>
        /// <exception cref="InvalidValueException">thrown if the Email is empty, null, doesn't have @ or '.'.</exception>
        public string Email
        {
            get => _email;
            set
            {
                _email = string.IsNullOrEmpty(value.Trim()) || !value.Contains("@") || !value.Contains(".")
                    ? throw new InvalidValueException("El email no puede estar vació y debe contener un dominio.")
                    : value.ToLower();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Getter and Setter for the Comment. If the comment is null or empty, set automatically "Sin comentario"
        /// </summary>
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = string.IsNullOrEmpty(value)
                    ? _comment
                    : value.Capitalize();
                OnPropertyChanged();
            }
        }
        public bool IsVip
        {
            get => _isVip;
            set
            {
                _isVip = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor for Customers.
        /// </summary>
        /// <exception cref="InvalidValueException">thrown when some value isn't accepted.</exception>
        /// <param name="name">string</param>
        /// <param name="surname">string</param>
        /// <param name="email">string</param>
        /// <param name="comment">string</param>
        /// <param name="isVip">bool, default false.</param>
        public Customer(string name, string surname,string city, string email, string comment, bool isVip = false)
        {
            Name = name;
            Surname = surname;
            City = city;
            Email = email;
            Comment = comment;
            IsVip = isVip;
        }

        public override string ToString() =>
            $"Customer(Name: {Name}, Surname: {Surname}, City: {City}, Email: {Email}, Comment: {Comment}, VIP: {IsVip})";

        /// <summary>
        /// Two customers are equal if they have the same email.
        /// </summary>
        /// <param name="other">Customer to compare if is equal.</param>
        /// <returns>boo, True if they are equal, false otherwise.</returns>
        public bool Equals(Customer? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Email == other.Email;
        }

        /// <summary>
        /// Two customers are equal if they have the same email.
        /// </summary>
        /// <param name="obj">Customer to compare if is equal.</param>
        /// <returns>boo, True if they are equal, false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Customer)obj);
        }

        public override int GetHashCode() => Email.GetHashCode();

        public static bool operator ==(Customer? left, Customer? right) => Equals(left, right);

        public static bool operator !=(Customer? left, Customer? right) => !Equals(left, right);

        /// <summary>
        /// Compare first by the name, then by the surname, then by the city, then by the email, then by if they are Vip, and finally by the comment.
        /// </summary>
        /// <param name="other">Customer to compare with.</param>
        /// <returns>int</returns>
        public int CompareTo(Customer? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;

            var nameComparison = string.Compare(_name, other._name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;

            var surnameComparison = string.Compare(_surname, other._surname, StringComparison.Ordinal);
            if (surnameComparison != 0) return surnameComparison;

            var emailComparison = string.Compare(_email, other._email, StringComparison.Ordinal);
            if (emailComparison != 0) return emailComparison;

            var cityComparison = string.Compare(_city, other._city, StringComparison.Ordinal);
            if (cityComparison != 0) return cityComparison;

            var isVipComparison = IsVip.CompareTo(other.IsVip);
            if (isVipComparison != 0) return isVipComparison;

            return string.Compare(_comment, other._comment, StringComparison.Ordinal);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
