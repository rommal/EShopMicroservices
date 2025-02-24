namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, IList<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
}
