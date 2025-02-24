namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException: ApplicationException
    {
        public ProductNotFoundException(): base("Product not found")
        {
            
        }
    }
}
