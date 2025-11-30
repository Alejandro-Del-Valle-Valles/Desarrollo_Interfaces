namespace SimulcaroExmane01.Model
{
    internal class Producto : IEquatable<Producto>
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }

        private int _stock = 0;

        public int Stock
        {
            get => _stock;
            set => _stock = value >= 0 ? value : 0;
        }

        public Producto(string nombre, string categoria, int stock)
        {
            Stock = stock;
            Nombre = nombre;
            Categoria = categoria;
        }

        public bool Equals(Producto? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Nombre == other.Nombre && Categoria == other.Categoria;
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
            return HashCode.Combine(Nombre, Categoria);
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
