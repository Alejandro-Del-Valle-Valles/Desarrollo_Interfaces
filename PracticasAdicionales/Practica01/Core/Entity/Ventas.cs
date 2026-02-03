namespace Practica01.Core.Entity
{
    /// <summary>
    /// Clase que representa las ventas de un mes
    /// </summary>
    internal class Venta : IEquatable<Venta>
    {
        public string Mes { get; set; }
        public decimal Total { get; set; }

        public Venta(string mes, decimal total)
        {
            Mes = mes;
            Total = total;
        }

        /// <summary>
        /// Dos Ventas son iguales si su mes es el mismo
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Venta? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Mes == other.Mes;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Venta)obj);
        }

        public override int GetHashCode() => Mes.GetHashCode();
        

        public static bool operator ==(Venta? left, Venta? right) => Equals(left, right);
        

        public static bool operator !=(Venta? left, Venta? right) => !Equals(left, right);
        
    }
}
