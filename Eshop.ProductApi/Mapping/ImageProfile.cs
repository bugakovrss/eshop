using AutoMapper;
using Eshop.ProductApi.Contracts;
using Eshop.Products.Model.Entities;

namespace Eshop.ProductApi.Mapping
{
    public class ImageProfile: Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, UploadImageResponse>();
            CreateMap<UploadImageRequest, Image>()
                .ForMember(d=>d.Name, opt=> opt.MapFrom(s=> s.Name))
                .ForMember(d => d.Data, opt => opt.ConvertUsing(new FormFileBytesConverter(), s=> s.Image))
                .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Image.ContentType));
        }
    }
}
