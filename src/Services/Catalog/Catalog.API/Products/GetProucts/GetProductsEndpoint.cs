
namespace Catalog.API.Products.GetProucts
{

    // will use pagination

    public record GetProductsReqeust(int? PageNumber = 1 , int? PageSize = 20);

    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Mish lazm ab3t l request 
            app.MapGet("/products",
                async ( [AsParameters] GetProductsQuery request , ISender sender) =>  
                { 

                    var Query = request.Adapt<GetProductsQuery>();
                    var result = await sender.Send(Query);
                    var response = result.Adapt<GetProductsResponse>();
                    return Results.Ok(response);
                                                                                                                 
                });

        }
    }
}
