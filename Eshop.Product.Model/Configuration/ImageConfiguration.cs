using Eshop.Products.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Products.Model.Configuration
{
    public class ImageConfiguration: IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images", "info");
            builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired();
            builder.Property(x => x.Created).HasColumnName(@"Created");
            builder.Property(x => x.Updated).HasColumnName(@"Updated");
            builder.Property(x => x.Version).IsRowVersion();
            builder.Property(x => x.Data).HasColumnName(@"Data").IsRequired();
            builder.Property(x => x.Type).HasColumnName(@"Type").IsRequired();
            builder.Property(x => x.Name).HasColumnName(@"Type");
        }
    }
}
