using System;

namespace Eshop.Products.Model.Entities
{
    public interface IProductEntity
    {
        long Id { get; set; }

        DateTime Created { get; set; }

        DateTime Updated { get; set; }

        byte[] Version { get; set; }
    }
}
