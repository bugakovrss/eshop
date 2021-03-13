using Eshop.Products.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Products.Model.Configuration
{
    public class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.ToTable("ProductGroups", "info");
            builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired();
            builder.Property(x => x.Created).HasColumnName(@"Created");
            builder.Property(x => x.Updated).HasColumnName(@"Updated");
            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.Name).HasColumnName(@"Name").IsRequired();
            builder.Property(x => x.ImageId).HasColumnName(@"ImageId");

            builder.HasOne(x => x.Image)
                .WithMany(x => x.ProductGroups)
                .HasForeignKey(x => x.ImageId);
        }
    }
}
