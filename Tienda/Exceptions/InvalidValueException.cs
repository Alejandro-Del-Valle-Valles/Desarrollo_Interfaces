namespace Tienda.Exceptions
{
    class InvalidValueException : Exception
    {
        private const string DefaultMessage = "The given value isn't valid.";
        public InvalidValueException() : base(DefaultMessage) { }
        public InvalidValueException(string message) : base(message) { }
        public InvalidValueException(Exception innerException) : base(DefaultMessage, innerException) {}
        public InvalidValueException(string message, Exception innerException) : base(DefaultMessage, innerException) { }
    }
}
