using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Eshop.ProductApi.Mapping
{
    public class FormFileBytesConverter: IValueConverter<IFormFile,byte[]>
    {
        public byte[] Convert(IFormFile sourceMember, ResolutionContext context)
        {
            using var ms = new MemoryStream();
            sourceMember.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
