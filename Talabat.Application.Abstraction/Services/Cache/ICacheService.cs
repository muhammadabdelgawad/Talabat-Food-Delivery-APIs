namespace Talabat.Application.Abstraction.Services.Cache
{
    public interface ICacheService
    {
        Task SetCacheAsync(string key, object response, TimeSpan timToLive);

        Task<string?> GetCacheAsync(string key);
    }
}
