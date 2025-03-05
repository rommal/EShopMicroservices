
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

            ShoppingCart? basket = null;

            if (!string.IsNullOrEmpty(cachedBasket))
            {
                basket = JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            }
            
            if (basket == null)
            {
                basket = await repository.GetBasket(userName, cancellationToken);
                await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            }                

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var result = await repository.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(result.UserName, JsonSerializer.Serialize(result), cancellationToken);

            return result;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var result = await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return result;
        }        
    }
}
