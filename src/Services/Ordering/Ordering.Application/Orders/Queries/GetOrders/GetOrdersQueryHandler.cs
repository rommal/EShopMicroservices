
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var request = query.PaginationRequest;
            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                .Include(e => e.OrderItems)                
                .OrderBy(e => e.OrderName.Value)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)                
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginatedResult<OrderDto>(request.PageIndex, request.PageSize, totalCount, orders.ToOrderDtoList()));
        }
    }
}
