using System.Xml.Linq;

namespace CatalogAPI.Products.CreateProduct
{
    internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command, cancellationToken);
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors.First());
            }

            Product product = new Product
            {
                Name = command.Name,
                Categories = command.Categories,
                Description = command.Descrption,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);            

            return new CreateProductResult(product.Id);
        }
    }
}
