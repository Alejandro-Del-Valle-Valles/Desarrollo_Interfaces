namespace Practica01.Core.Exceptions
{
    /// <summary>
    /// Excpeción que se lanza cuando ya existe una venta con el mes asignado.
    /// </summary>
    internal class VentaAlreadyExistsException : Exception
    {
        private const string DefaultMessage = "Ya existe una venta con ese mes";
        public VentaAlreadyExistsException() : base(DefaultMessage) { }
        public VentaAlreadyExistsException(string message) : base(message) { }
        public VentaAlreadyExistsException(Exception ex) : base(DefaultMessage, ex) { }
        public VentaAlreadyExistsException(string message, Exception ex) : base(message, ex) { }
    }
}
