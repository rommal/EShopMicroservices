namespace CatalogAPI.Products.GetProductByCategory
{
    internal class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQuery.Handler called with {@Query}", query);

            var product = await session.Query<Product>()
                .Where(x => x.Categories.Contains(query.Category))
                .ToListAsync();

            return new GetProductsByCategoryResult(product.ToList());
        }
    }
}
