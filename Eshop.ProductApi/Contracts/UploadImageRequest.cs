using Microsoft.AspNetCore.Http;

namespace Eshop.ProductApi.Contracts
{
    public record UploadImageRequest
    {
        public IFormFile Image { get; init; }

        public string Name { get; init; }
    }
}
