using Application.Data;
using Application.Products.Get;
using Domain.Products.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.GetAll;
internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsResponse>
{
    private readonly IApplicationDbContext _context;

    public GetAllProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductsResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context
             .Products
             .Select(x => new ProductResponse(
                 x.Id.Value,
                 x.Name,
                 x.Sku.Value,
                 x.Price.Currency,
                 x.Price.Amount))
             .ToListAsync(cancellationToken);

        return new ProductsResponse(items);
    }
}