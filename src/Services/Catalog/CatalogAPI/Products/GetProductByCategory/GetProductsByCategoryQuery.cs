namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string Category): IQuery<GetProductsByCategoryResult>;
}
