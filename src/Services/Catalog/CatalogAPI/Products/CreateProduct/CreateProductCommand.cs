using MediatR;

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, IList<string> Categories, string Descrption, string ImageFile, decimal Price): IRequest<CreateProductResult>;
}
