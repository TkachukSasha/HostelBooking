namespace Hostel.Shared.Types.Cache
{
    public interface ICacheRepository
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan lifeTime);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
