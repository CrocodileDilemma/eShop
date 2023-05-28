using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.GetAll;
using Carter;
using Domain.Products;
using Domain.Products.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Endpoints;

public class ProductsModule : CarterModule
{
    public ProductsModule() : base("/products")
    {

    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (ISender sender) =>
        {
            return Results.Ok(await sender.Send(new GetAllProductsQuery()));
        });

        app.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var query = new GetProductQuery(new ProductId(id));
                return Results.Ok(await sender.Send(query));
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapPost("/", async (CreateProductCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            try
            {
                var command = new UpdateProductCommand(
                    new ProductId(id),
                    request.Name,
                    request.Sku,
                    request.Currency,
                    request.Amount);

                await sender.Send(command);
                return Results.NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteProductCommand(new ProductId(id)));
                return Results.NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
    }
}
