using ModoConectado.Extenisons;

namespace ModoConectado.Model
{
    class Department : IEquatable<Department>
    {
        private int _id = 0;
        public int Id
        {
            get => _id;
            set => _id = value > 0 ? value : _id;
        }

        private string _name = "Desconocido";
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? _name.Capitalize() : value.Trim();
        }

        private string _localization = "Desconocida";

        public string Localization
        {
            get => _localization;
            set => _localization = string.IsNullOrWhiteSpace(value) ? _localization.Capitalize() : value.Trim();
        }

        public Department(int id, string name, string localization)
        {
            Id = id;
            Name = name;
            Localization = localization;
        }

        public Department(string name, string localization)
        : this(0, name, localization)
        { }

        public bool Equals(Department? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _name == other._name && _localization == other._localization;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Department)obj);
        }

        public override int GetHashCode() => HashCode.Combine(_name, _localization);

        public static bool operator ==(Department? left, Department? right) => Equals(left, right);

        public static bool operator !=(Department? left, Department? right) => !Equals(left, right);
    }
}
