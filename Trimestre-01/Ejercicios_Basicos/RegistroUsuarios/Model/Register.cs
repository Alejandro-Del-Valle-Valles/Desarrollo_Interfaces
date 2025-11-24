using RegistroUsuarios.Exceptions;
using RegistroUsuarios.Extensions;

namespace RegistroUsuarios.Model
{
    internal class Register : IEquatable<Register>, IComparable<Register>
    {
        private string _name = "Desconocio";

        public string Name
        {
            get => _name;
            set => _name = value.Capitalize();
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains('@') || !value.Contains('.'))
                    throw new EmailNotValidException();
                else _email = value.Trim().ToLower();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => _password = value.Trim();
        }

        public Register() { }

        public Register(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Two registers are equal if they have the same email.
        /// </summary>
        /// <param name="other">Register to compare with.</param>
        /// <returns>bool, true if they are equal, false otherwise.</returns>
        public bool Equals(Register? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _email == other._email;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Register)obj);
        }

        public override int GetHashCode() => _email.GetHashCode();

        public static bool operator ==(Register? left, Register? right) => Equals(left, right);
        
        public static bool operator !=(Register? left, Register? right) => !Equals(left, right);

        public int CompareTo(Register? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;

            var nameComparison = string.Compare(_name, other._name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;

            return string.Compare(_email, other._email, StringComparison.Ordinal);
        }
    }
}
