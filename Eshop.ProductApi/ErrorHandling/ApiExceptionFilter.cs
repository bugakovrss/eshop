using Eshop.Core.Extensions.ErrorHandling;
using Microsoft.Extensions.Logging;

namespace Eshop.ProductApi.ErrorHandling
{
    /// <summary>
    /// Exception filter
    /// </summary>
    public class ApiExceptionFilter: ApiExceptionFilterBase<ErrorCode>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public ApiExceptionFilter(ILogger<ApiExceptionFilterBase<ErrorCode>> logger) : base(logger)
        {
        }

        /// <inheritdoc />
        protected override ErrorCode GetUnspecifiedErrorCode()
        {
            return ErrorCode.Unknown;
        }
    }
}
