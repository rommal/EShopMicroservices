﻿namespace Ordering.Domain.Models
{
    public class OrderItem: Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderId OrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
