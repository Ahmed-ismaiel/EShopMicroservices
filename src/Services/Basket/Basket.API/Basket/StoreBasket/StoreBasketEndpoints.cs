
namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketRequest(ShoppingCart ShoppingCart) ;

    public record StoreBasketResponse(string userName);
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (ShoppingCart shoppingCart , ISender sender) => 
            {
            
                var command = shoppingCart.Adapt<StoreBaskeCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>() ;
                return Results.Created($"/basket/{response.userName}", response);





            });
        }
    }
}
