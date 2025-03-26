using BuildingBlocks.Messaging.Events;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Models;
using Ordering.Domain.Models.Enums;

namespace Ordering.Application.Extensions
{
    internal static class OrderExtensions
    {
        public static IList<OrderDto> ToOrderDtoList(this IList<Order> orders) 
        {
            return orders.Select(order => order.ToOrderDto()).ToList();
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            return DtoFromOrder(order);
        }

        public static CreateOrderCommand ToCreateOrderCommand(this BasketCheckoutEvent message)
        {
            var orderId = Guid.NewGuid();
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress!, message.AddressLine, message.Country, message.State, message.ZipCode);
            var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);

            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: OrderStatus.Pending,
                OrderItems:
                [
                    new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                    new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
                ]);

            return new CreateOrderCommand(orderDto);
        }

        private static OrderDto DtoFromOrder(Order order)
        {
            return new OrderDto(
                        Id: order.Id.Value,
                        CustomerId: order.CustomerId.Value,
                        OrderName: order.OrderName.Value,
                        ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                        BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
                        Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                        Status: order.Status,
                        OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                    );
        }
    }
}
