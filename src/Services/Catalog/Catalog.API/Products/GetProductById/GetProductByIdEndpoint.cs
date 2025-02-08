
namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponce(Product Product);



    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapGet("/product/{id}",
                async (Guid id, ISender sender) =>
                {
                    var Query = await sender.Send(new GetProductByIdQuery(id));
                    var response = Query.Adapt<GetProductByIdResponce>();
                    return Results.Ok(response);
                });

        }
    }

}
