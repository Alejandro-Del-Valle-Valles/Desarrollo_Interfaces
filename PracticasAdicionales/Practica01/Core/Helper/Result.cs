namespace Practica01.Core.Helper
{
    /// <summary>
    /// Esta clase permite encapsular el resultado de una operación del servicio.
    /// Si algo falla, se llama a Failure donde se introduce la excepción producida y se autosetea el IsSucces a False
    /// Si todo sale bien, se llama a Success y se autosetea el IsSuccess a True y la excepción a null
    /// </summary>
    internal class Result
    {
        public bool IsSuccess { get; set; }
        public Exception? Exception { get; set; }

        public static Result Success() => new Result
        {
            IsSuccess = true,
            Exception = null,
        };

        public static Result Failure(Exception ex) => new Result
        {
            IsSuccess = false,
            Exception = ex
        };
    }

    /// <summary>
    /// Esta clase permite encapsular el resultado de una operación del servicio.
    /// Si algo falla, se llama a Failure donde se introduce la excepción producida y se autosetea el IsSucces a False y los datos a null
    /// Si todo sale bien, se llama a Success y se autosetea el IsSuccess a True, la excepción a null y se pasa el dato a guardar.
    /// </summary>
    internal class Result<T>
    {
        public bool IsSuccess { get; set; }
        public Exception? Exception { get; set; }
        public T? Data { get; set; }

        public static Result<T> Success(T? data) => new Result<T>
        {
            IsSuccess = true,
            Exception = null,
            Data = data == null ? default : data
        };

        public static Result<T> Failure(Exception ex) => new Result<T>
        {
            IsSuccess = false,
            Exception = ex,
            Data = default
        };
    }
}
