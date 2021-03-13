using System;
using System.Collections.Generic;

namespace Eshop.Products.Model.Entities
{
    public class ProductGroup: IProductEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public byte[] Version { get; set; }

        public string Name { get; set; }

        public long? ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public ProductGroup()
        {
            Products = new HashSet<Product>();
        }
    }
}
