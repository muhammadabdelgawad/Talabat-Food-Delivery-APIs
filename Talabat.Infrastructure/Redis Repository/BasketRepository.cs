using StackExchange.Redis;
using System.Text.Json;
namespace Talabat.Infrastructure.Redis_Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _datbase;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _datbase = redis.GetDatabase();
        }
        public async Task<Basket?> GetAsync(string id)
        {
            var basket= await _datbase.StringGetAsync(id);
            return  basket.IsNullOrEmpty? null: JsonSerializer.Deserialize<Basket>(basket!);
        }
        
        public async Task<Basket?> UpdateAsync(Basket basket, TimeSpan timeToLive)
        {
            var SerializedBasket = JsonSerializer.Serialize(basket);
            var updated = await _datbase.StringSetAsync(basket.Id, SerializedBasket, timeToLive);
            if(!updated) return null;
            return basket;
        }

        public async Task DeleteAsync(string id)=> await _datbase.KeyDeleteAsync(id);

    }
}
