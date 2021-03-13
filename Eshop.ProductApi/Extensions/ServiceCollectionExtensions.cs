using Eshop.Products.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.ProductApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInMemoryContext(this IServiceCollection services)
        {
            services.AddDbContext<ProductContext>(
                builder => builder.UseInMemoryDatabase("ProductDb"));
        }
    }
}
