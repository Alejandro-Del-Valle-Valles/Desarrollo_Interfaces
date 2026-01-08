namespace ModoConectado.Exceptions
{
    internal class ObjectNotInsertedUpdatedDeletedException : Exception
    {
        private const string DEFAULT_MESSAGE = "The object wasn't inserted | updated | deleted";
        public ObjectNotInsertedUpdatedDeletedException() : base(DEFAULT_MESSAGE) { }
        public ObjectNotInsertedUpdatedDeletedException(string message) : base(message) { }
        public ObjectNotInsertedUpdatedDeletedException(Exception ex) : base(DEFAULT_MESSAGE, ex) { }
        public ObjectNotInsertedUpdatedDeletedException(string message, Exception ex) : base(message, ex) {}
    }
}
