namespace SimulacroExamen.Entity
{
    public class Producto : IEquatable<Producto>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }

        public Producto(string nombre, string descripcion, float precio)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
        }

        public bool Equals(Producto? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Nombre == other.Nombre;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Producto)obj);
        }

        public override int GetHashCode()
        {
            return Nombre.GetHashCode();
        }

        public static bool operator ==(Producto? left, Producto? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Producto? left, Producto? right)
        {
            return !Equals(left, right);
        }
    }
}
