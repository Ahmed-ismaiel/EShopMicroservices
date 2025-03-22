

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            // Hena hageb l shoping cart mn el database 3n tare2 el user name
            var basket =  await session.LoadAsync<ShoppingCart>(userName , cancellationToken);
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            // mezet Martin any 22dr a3ml Upsert 3la el shopping cart lw msh mwgoda insert lw mwgoda update

            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;

        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
