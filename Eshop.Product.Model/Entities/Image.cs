using System;
using System.Collections.Generic;

namespace Eshop.Products.Model.Entities
{
    public class Image: IProductEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public byte[] Version { get; set; }

        public byte[] Data { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public Image()
        {
            ProductGroups = new HashSet<ProductGroup>();
            Products = new HashSet<Product>();
        }
    }
}
