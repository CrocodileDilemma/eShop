using Application.Data;
using Domain.Products.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Get;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductResponse>
{
    private readonly IApplicationDbContext _context;

    public GetProductQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .Where(x => x.Id == request.ProductId)
            .Select(x => new ProductResponse(
                x.Id.Value,
                x.Name,
                x.Sku.Value,
                x.Price.Currency,
                x.Price.Amount))
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        return product;
    }
}
