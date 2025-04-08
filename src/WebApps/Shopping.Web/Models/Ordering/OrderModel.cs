﻿namespace Shopping.Web.Models.Ordering
{
    public record OrderModel (
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressModel ShippingAddress,
        AddressModel BillingAddress,
        PaymentModel Payment,
        OrderStatus Status,
        List<OrderItemModel> OrderItems);

    public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);
    public record GetOrdersByNameResponse(IList<OrderModel> Orders);
    public record GetOrdersByCustomerResponse(IList<OrderModel> Orders);
}
