namespace Tareas.Model
{
    internal class Result
    {
        public bool IsSuccess { get; private set; }
        public Exception? Exception { get; private set; }

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

    internal class Result<T>
    {
        public T? Object { get; private set; }
        public bool IsSuccess { get; private set; }
        public Exception? Exception { get; private set; }

        public static Result<T?> Success(T? obj) => new Result<T?>
        {
            Object = obj,
            IsSuccess = true,
            Exception = null,
        };

        public static Result<T?> Failure(Exception ex) => new Result<T?>
        {
            Object = default(T),
            IsSuccess = false,
            Exception = ex
        };
    }
}
