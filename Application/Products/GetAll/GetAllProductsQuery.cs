using Application.Products.Get;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetAll;

public record GetAllProductsQuery: IRequest<ProductsResponse>;

public record ProductsResponse(List<ProductResponse> Products);
