
using Marten;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository 
        , IDistributedCache cache ) 
        : IBasketRepository
    {

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            // Hena 3mlt inject ll Distributed cache 3ashan ageb l data l awl mn cashe 
            // w lw l basket mwgoda fl cashe 3mlt return mn el cashe

            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            // hyrg3ly el basket mn el cashe fe shakl json owe ana h7wl el json l ShoppingCart
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            }

            //If the cache is empty(cache miss), the method fetches the shopping cart from a repository (e.g., a database) using repository.GetBasket.
            //This is also an async call, awaited to retrieve the ShoppingCart object.
            //The retrieved basket is serialized to a JSON string using JsonSerializer.Serialize(basket) and stored in the cache with cache.SetStringAsync, using userName as the key.
            //Finally, the basket is returned to the caller.

            var basket = await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            // Hena b2a b3d ma a3ml Store l basket fl database 3mlt h3ml add fl cache brdo
            
             await repository.StoreBasket(basket, cancellationToken);
             await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {        // b3d ma a3ml delete l basket mn el database 3mlt delete l basket mn el cache
            await repository.DeleteBasket(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }

    }
}
