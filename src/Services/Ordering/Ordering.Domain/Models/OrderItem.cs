namespace Ordering.Domain.Models
{
    public class OrderItem: Entity<Guid>
    {
        internal OrderItem(Guid orderId, Guid productId, int quantity, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
