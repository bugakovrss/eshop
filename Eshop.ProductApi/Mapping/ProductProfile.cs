using AutoMapper;
using Eshop.ProductApi.Contracts;
using Eshop.Products.Model.Entities;

namespace Eshop.ProductApi.Mapping
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();
            CreateMap<CreateProductRequest, Product>();
        }
    }
}
