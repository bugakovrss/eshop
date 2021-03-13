using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Eshop.Core.Extensions.ErrorHandling
{
    public abstract class ApiExceptionFilterBase<T>: IAsyncExceptionFilter where T: Enum
    {
        private readonly ILogger<ApiExceptionFilterBase<T>> _logger;

        protected ApiExceptionFilterBase(ILogger<ApiExceptionFilterBase<T>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handle exception
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public  Task OnExceptionAsync(ExceptionContext context)
        {
            try
            {
                AppError<T> error;

                HttpStatusCode code;
                _logger.LogError(context.Exception, "Error happened");

                if (context.Exception is ApiException<T> apiEx)
                {
                    error = apiEx.GetError();
                    code = HttpStatusCode.BadRequest;
                }
                else
                {
                    code = HttpStatusCode.InternalServerError;
                    error = new AppError<T>
                    {
                        Message = context.Exception.Message,
                        Code = GetUnspecifiedErrorCode()
                    };
                }

                context.Result = new BadRequestObjectResult(error)
                {
                    StatusCode = (int) code
                };

            }
            catch (Exception e)
            {

                _logger.LogError(e, "Error in exception filter");
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Get default error code
        /// </summary>
        /// <returns></returns>
        protected abstract T GetUnspecifiedErrorCode();
    }
}
