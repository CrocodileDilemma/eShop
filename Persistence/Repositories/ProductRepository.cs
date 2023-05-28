using Domain.Products;

namespace Persistence.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public async Task<Product?> GetByIdAsync(ProductId id)
    {
        return await _context.Products.FindAsync(id);
    }

    public void Remove(Product product)
    {
        _context.Remove(product);
    }
}
