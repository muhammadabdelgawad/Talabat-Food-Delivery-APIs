using StackExchange.Redis;
using System.Text.Json;
using Talabat.Application.Abstraction.Services.Cache;

namespace Talabat.Infrastructure.Cache
{
    public class CacheService(IConnectionMultiplexer redis) : ICacheService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        
        public async Task SetCacheAsync(string key, object response, TimeSpan timToLive)
        {
            if (response is null) return;
            
            var serializedOptions= new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            
            var serializedResponse = JsonSerializer.Serialize(response, serializedOptions);

            await _database.StringSetAsync(key, serializedResponse, timToLive);
        }


        public async Task<string?> GetCacheAsync(string key)
        {
            var reponse = await _database.StringGetAsync(key);
            if (reponse.IsNull) return null;
            
            return reponse;
        }

    }
}
