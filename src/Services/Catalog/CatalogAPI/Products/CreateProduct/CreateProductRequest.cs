namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductRequest(string Name, IList<string> Categories, string Descrption, string ImageFile, decimal Price);
}
