using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Products.Model.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Entities.Product> builder)
        {
            builder.ToTable("Products", "info");
            builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired();
            builder.Property(x => x.Created).HasColumnName(@"Created");
            builder.Property(x => x.Updated).HasColumnName(@"Updated");
            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.Name).HasColumnName(@"Name").IsRequired();
            builder.Property(x => x.Description).HasColumnName(@"Description");
            builder.Property(x => x.ImageId).HasColumnName(@"ImageId");
            builder.Property(x => x.ProductGroupId).HasColumnName(@"ProductGroupId").IsRequired();
            builder.Property(x => x.AvailableCount).HasColumnName(@"AvailableCount").IsRequired();
            builder.Property(x => x.TotalCount).HasColumnName(@"TotalCount").IsRequired();


            builder.HasOne(x => x.Image)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ImageId);

            builder.HasOne(x => x.ProductGroup)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductGroupId);
        }
    }
}
