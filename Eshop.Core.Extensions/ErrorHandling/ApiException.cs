using System;

namespace Eshop.Core.Extensions.ErrorHandling
{
    public class ApiException<T>: Exception where T: Enum
    {
        private readonly T _errorCode;

        public ApiException(T errorCode, string message):base(message)
        {
            _errorCode = errorCode;
        }

        public AppError<T> GetError()
        {
            return new AppError<T>
            {
                Message = base.Message,
                Code = _errorCode
            };
        }
    }
}
