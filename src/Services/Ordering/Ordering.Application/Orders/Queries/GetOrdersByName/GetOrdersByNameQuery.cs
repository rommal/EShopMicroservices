namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public record GetOrdersByNameQuery(string Name): IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IList<OrderDto> Orders);
}
