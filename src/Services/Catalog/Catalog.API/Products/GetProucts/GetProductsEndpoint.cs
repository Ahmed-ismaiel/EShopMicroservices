
namespace Catalog.API.Products.GetProucts
{

   // public record GetProductsReqeust();
      
      public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Mish lazm ab3t l request 
            app.MapGet("/products",
                async ( ISender sender) =>  
                { 
                    var Query = await sender.Send(new GetProductsQuery());
                    var response = Query.Adapt<GetProductsResponse>();
                    return Results.Ok(response);

                });

        }
    }
}
