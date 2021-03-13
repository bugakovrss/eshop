using Eshop.Core.Extensions.ErrorHandling;

namespace Eshop.ProductApi.ErrorHandling
{
    /// <summary>
    /// Exception factory
    /// </summary>
    public static class ErrorFactory
    {
        /// <summary>
        /// Create exception
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Description exception</param>
        /// <returns></returns>
        public static ApiException<ErrorCode> Create(ErrorCode code, string message)
        {
            return new(code, message);
        }
    }
}
