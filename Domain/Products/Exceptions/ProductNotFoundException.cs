namespace Domain.Products.Exceptions;

public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(ProductId id)
        : base($"Product with ID: {id.Value} was not found.") 
    {
    }
}
