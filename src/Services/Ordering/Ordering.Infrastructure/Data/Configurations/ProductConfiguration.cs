﻿
namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasConversion(productId => productId.Value, dbId => ProductId.Of(dbId));

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();            
        }
    }
}
