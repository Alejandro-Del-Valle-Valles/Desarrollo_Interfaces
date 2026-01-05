using ModoConectado.Extenisons;

namespace ModoConectado.Model
{
    class Employee : IEquatable<Employee>, IComparable<Employee>
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
            set => _name = string.IsNullOrWhiteSpace(value) ? _name : value.CapitalizeAll();
        }

        private string _surname = "Desconocido";
        public string Surname
        {
            get => _surname;
            set => _surname = string.IsNullOrWhiteSpace(value) ? _surname : value.CapitalizeAll();
        }

        private string _craft = "Desconocido";
        public string Craft
        {
            get => _craft;
            set => _craft = string.IsNullOrWhiteSpace(value) ? _craft : value.Capitalize();
        }

        private float _salary = 0f;
        public float Salary
        {
            get => _salary;
            set => _salary = value > 0f ? value : _salary;
        }

        private float _commission = 0f;
        public float Commission
        {
            get => _commission;
            set => _commission = value > 0f ? value : _commission;
        }

        private string _registrationDate = DateTime.Now.ToString("dd-MM-yyyy");
        public string RegistrationDate
        {
            get => _registrationDate;
            set
            {
                bool isParsed = DateTime.TryParse(value.Trim(), out DateTime date);
                _registrationDate = isParsed 
                    ? date.ToString("dd-MM-yyyy") 
                    : DateTime.Now.ToString("dd-MM-yyyy");
            }
        }

        private int _idDepartment = 0;
        public int IdDepartment
        {
            get => _idDepartment;
            set => _idDepartment = value > 0 ? value : _idDepartment;
        }

        public Employee(int id, string name, string surname, string craft, float salary, float commission, int idDepartment)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Craft = craft;
            Salary = salary;
            Commission = commission;
            IdDepartment = idDepartment;
        }

        public Employee(string name, string surname, string craft, float salary, float commission, int idDepartment)
        {
            Name = name;
            Surname = surname;
            Craft = craft;
            Salary = salary;
            Commission = commission;
            IdDepartment = idDepartment;
        }

        public bool Equals(Employee? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Employee)obj);
        }

        public override int GetHashCode() => _id;
        public static bool operator ==(Employee? left, Employee? right) =>  Equals(left, right);
        public static bool operator !=(Employee? left, Employee? right) => !Equals(left, right);

        public int CompareTo(Employee? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;

            var nameComparison = string.Compare(_name, other._name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;

            var surnameComparison = string.Compare(_surname, other._surname, StringComparison.Ordinal);
            if (surnameComparison != 0) return surnameComparison;

            var craftComparison = string.Compare(_craft, other._craft, StringComparison.Ordinal);
            if (craftComparison != 0) return craftComparison;

            var salaryComparison = _salary.CompareTo(other._salary);
            if (salaryComparison != 0) return salaryComparison;

            var commissionComparison = _commission.CompareTo(other._commission);
            if (commissionComparison != 0) return commissionComparison;

            return string.Compare(_registrationDate, other._registrationDate, StringComparison.Ordinal);
        }
    }
}
