using Tienda.Enums;

namespace Tienda.Model
{
    /// <summary>
    /// Class used for boolean return values to save if the operations went well or not.
    /// </summary>
    internal class ServiceResult
    {
        public bool IsSuccess { get; private set; }
        public ServiceErrorType Error { get; private set; }
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Used when the operation went well.
        /// </summary>
        /// <returns>Service Result without error and IsSuccess equal to true.</returns>
        public static ServiceResult Success()
        {
            return new ServiceResult
            {
                IsSuccess = true,
                ErrorMessage = "None",
                Error = ServiceErrorType.None
            };
        }

        /// <summary>
        /// Used when the operation went bad.
        /// </summary>
        /// <param name="error">ServiceErrorType with the type of error</param>
        /// <param name="message">string error message.</param>
        /// <returns>Service Result with error and IsSuccess equal to false.</returns>
        public static ServiceResult Failure(ServiceErrorType error, string message)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                ErrorMessage = message,
                Error = error
            };
        }
    }

    /// <summary>
    /// Class used to save objects to be used later or errors inf something went wrong while the program tries to obtain the object.
    /// </summary>
    /// <typeparam name="T">Object to work with.</typeparam>
    internal class ServiceResult<T>
    {
        public T? Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public ServiceErrorType Error { get; private set; }
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Used when the operation went well.
        /// </summary>
        /// <param name="data">data to be saved for use.</param>
        /// <returns>Service Result without error and IsSuccess equal to true.</returns>
        public static ServiceResult<T> Success(T? data)
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Data = data,
                ErrorMessage = "None",
                Error = ServiceErrorType.None
            };
        }

        /// <summary>
        /// Used when the operation went well.
        /// </summary>
        /// <param name="error">ServiceErrorType with the type of error</param>
        /// <param name="message">string error message.</param>
        /// <returns>Service Result without error and IsSuccess equal to true.</returns>
        public static ServiceResult<T> Failure(ServiceErrorType error, string message)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                Data = default(T),
                ErrorMessage = message,
                Error = error
            };
        }
    }
}
