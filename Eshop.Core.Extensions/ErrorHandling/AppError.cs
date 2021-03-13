using System;

namespace Eshop.Core.Extensions.ErrorHandling
{
    /// <summary>
    /// Application error model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AppError<T> where T: Enum
    {
        /// <summary>
        /// Error code
        /// </summary>
        public T Code { get; set; }

        /// <summary>
        /// Description of error
        /// </summary>
        public string Message { get; set; }
    }
}
