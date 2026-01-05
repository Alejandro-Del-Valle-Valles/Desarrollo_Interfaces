namespace ModoConectado.Model
{
    class Result
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

    class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public Exception? Exception { get; private set; }
        public T? Data { get; private set; }
        public static Result<T> Success(T? data) => new Result<T>
        {
            IsSuccess = true,
            Data = data != null ? data : default(T),
            Exception = null
        };

        public static Result<T> Failure(Exception ex) => new Result<T>
        {
            IsSuccess = false,
            Data = default(T),
            Exception = ex
        };
    }
}
