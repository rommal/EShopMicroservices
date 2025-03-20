
namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateCommandResult>
    {
        public async Task<UpdateCommandResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderDto = command.Order;

            var orderId = OrderId.Of(orderDto.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

            if (order == null) 
            {
                throw new OrderNotFoundException(orderId.Value);
            }

            UpdateOrder(orderDto, order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateCommandResult(true);
        }

        private Order UpdateOrder(OrderDto orderDto, Order updatedOrder)
        {
            var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAdress,
                orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAdress,
                orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
            var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            updatedOrder.Update(
                OrderName.Of(orderDto.OrderName),
                shippingAddress,
                billingAddress,
                payment,
                orderDto.Status);

            return updatedOrder;
        }
    }
}
