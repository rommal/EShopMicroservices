namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order): ICommand<UpdateCommandResult>;

    public record UpdateCommandResult(bool IsSuccess);
}
