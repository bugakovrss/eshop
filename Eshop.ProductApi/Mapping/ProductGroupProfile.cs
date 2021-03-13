using AutoMapper;
using Eshop.ProductApi.Contracts;
using Eshop.Products.Model.Entities;

namespace Eshop.ProductApi.Mapping
{
    public class ProductGroupProfile: Profile
    {
        public ProductGroupProfile()
        {
            CreateMap<ProductGroupModel, ProductGroup>();
            CreateMap<ProductGroup, ProductGroupModel>();
            CreateMap<CreateProductGroupRequest, ProductGroup>();
        }
    }
}
