namespace RegistroUsuarios.Exceptions
{
    internal class EmailNotValidException : Exception
    {
        private const string Message = "El email introducido no es válido. Debe contener un dominio (@ y .)";
        public EmailNotValidException() : base(Message) { }
        public EmailNotValidException(Exception ex) : base(Message, ex) { }
        public EmailNotValidException(string message) : base(message) { }
        public EmailNotValidException(string message, Exception ex) : base(message, ex) { }
    }
}
