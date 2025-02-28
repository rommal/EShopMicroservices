namespace CatalogAPI.Products.GetProductByCategory
{
    internal class GetProductsByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>()
                .Where(x => x.Categories.Contains(query.Category))
                .ToListAsync();

            return new GetProductsByCategoryResult(product.ToList());
        }
    }
}
