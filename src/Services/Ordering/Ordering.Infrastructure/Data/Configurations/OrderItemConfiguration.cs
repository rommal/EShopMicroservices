﻿
namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasConversion(orderItemId => orderItemId.Value, dbId => OrderItemId.Of(dbId));

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(e => e.ProductId);

            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Price).IsRequired();
        }
    }
}
