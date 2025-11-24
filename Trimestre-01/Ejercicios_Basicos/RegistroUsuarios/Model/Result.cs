namespace RegistroUsuarios.Model
{
    internal class Result
    {
        public bool IsSuccess { get; private set; }
        public Exception? Exception { get; private set; }

        public static Result Success() => new Result
        {
            IsSuccess = true,
            Exception = null
        };

        public static Result Failure(Exception ex) => new Result
        {
            IsSuccess = false,
            Exception = ex
        };
    }

    internal class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public Exception? Exception { get; private set; }
        public T? Value { get; private set; }

        public static Result<T> Success(T? value) => new Result<T>
        {
            IsSuccess = true,
            Value = value,
            Exception = null
        };

        public static Result<T> Failure(Exception ex) => new Result<T>
        {
            IsSuccess = false,
            Value = default(T),
            Exception = ex
        };
    }
}
