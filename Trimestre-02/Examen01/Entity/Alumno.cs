namespace Examen01.entity
{
    public class Alumno : IEquatable<Alumno>
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public float NotaMedia { get; set; }
        public DateOnly FechaNacimiento { get; set; }

        public Alumno(string nombre, float notaMedia, DateOnly fechaNacimiento)
        {
            Nombre = nombre;
            NotaMedia = notaMedia;
            FechaNacimiento = fechaNacimiento;
        }

        public Alumno(int id, string nombre, float notaMedia, DateOnly fechaNacimiento)
        {
            Id = id;
            Nombre = nombre;
            NotaMedia = notaMedia;
            FechaNacimiento = fechaNacimiento;
        }

        public bool Equals(Alumno? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Alumno)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Alumno? left, Alumno? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Alumno? left, Alumno? right)
        {
            return !Equals(left, right);
        }
    }
}
