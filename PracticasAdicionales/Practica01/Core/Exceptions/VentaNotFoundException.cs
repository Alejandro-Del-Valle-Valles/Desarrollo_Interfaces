namespace Practica01.Core.Exceptions
{
    /// <summary>
    /// Excepción lanzada cuando no se encuentra la venta buscada.
    /// </summary>
    internal class VentaNotFoundException : Exception
    {
        private const string DefaultMessage = "No hay ninguna venta con el mes buscado";

        public VentaNotFoundException() : base(DefaultMessage) { }
        public VentaNotFoundException(string message) : base(message) { }
        public VentaNotFoundException(Exception ex) : base(DefaultMessage, ex) { }
        public VentaNotFoundException(string message, Exception ex) : base (message, ex) {}
    }
}
