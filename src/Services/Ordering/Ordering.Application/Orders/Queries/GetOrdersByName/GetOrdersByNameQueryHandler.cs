﻿using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(e => e.OrderItems)
                .AsNoTracking()
                .Where(e => e.OrderName.Value .Contains(query.Name))
                .OrderBy(e => e.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }        
    }
}
