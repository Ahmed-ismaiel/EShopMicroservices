
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
              
            app.MapDelete("/products/{id}",
                async (ISender sender, Guid id) =>
                {
                    var command = new DeleteProductCommand(id);
                    var result = await sender.Send(command);
                    var response = result.Adapt<DeleteProductResponse>();
                    return Results.Ok(response);
                });

        }
    }
}
