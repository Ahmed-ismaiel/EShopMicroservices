
namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProductByCategoryRequest(string CategoryId);
    public record GetProductByCategoryResponse(IEnumerable<Product> Product);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/category/{categoryId}"
               , async (ISender sender , string categoryId) =>
           {
                var query = await sender.Send(new GetProductByCategoryQuery(categoryId));
               var response = query.Adapt<GetProductByCategoryResponse>();
               return Results.Ok(response);
           });
        }
    }
}
