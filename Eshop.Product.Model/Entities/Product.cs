using System;

namespace Eshop.Products.Model.Entities
{
    public class Product: IProductEntity
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public byte[] Version { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long AvailableCount { get; set; }

        public long TotalCount { get; set; }

        public long ProductGroupId { get; set; }

        public long? ImageId { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }

        public virtual Image Image { get; set; }
    }
}
