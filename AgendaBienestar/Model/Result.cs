using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaBienestar.Model
{
    /// <summary>
    /// Class used to save if the operations were well or not and the exception if something went wrong.
    /// </summary>
    internal class Result
    {
        public bool IsSuccess { get; private set; }
        public Exception? Exception { get; private set; }

        /// <summary>
        /// Used when te operation went well.
        /// </summary>
        /// <returns>Result without Exception and IsSuccess true.</returns>
        public static Result Success() => new Result
        {
            IsSuccess = true,
            Exception = null
        };

        /// <summary>
        /// Used when te operation went wrong.
        /// </summary>
        /// <returns>Result with Exception and IsSuccess false.</returns>
        public static Result Failure(Exception ex) => new Result
        {
            IsSuccess = false,
            Exception = ex
        };
    }

    /// <summary>
    /// Class used to save the data and if the operations were well or not and the exception if something went wrong.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Result<T>
    {
        public T? Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public Exception? Exception { get; private set; }

        /// <summary>
        /// Used when te operation went well.
        /// </summary>
        /// <returns>Result with the Dat, without Exception and IsSuccess true.</returns>
        public static Result<T> Success(T? data) => new Result<T>
        {
            Data = data,
            IsSuccess = true,
            Exception = null
        };

        /// <summary>
        /// Used when te operation went wrong.
        /// </summary>
        /// <returns>Result with default Data, Exception and IsSuccess false.</returns>
        public static Result<T> Failure(T? data, Exception? ex) => new Result<T>
        {
            Data = default(T),
            IsSuccess = false,
            Exception = ex
        };
    }
}
