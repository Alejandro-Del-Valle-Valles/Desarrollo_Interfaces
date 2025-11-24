namespace Tienda.Enums
{
    /// <summary>
    /// Enum that contains the errors that can be thrown in services.
    /// This is to specify the error thrown into ServiceResult.
    /// </summary>
    internal enum ServiceErrorType
    {
        None,
        UnknowException,
        InvalidValueException,
        IOException,
        ArgumentException,
        DirectoryNotFoundException
    }
}
