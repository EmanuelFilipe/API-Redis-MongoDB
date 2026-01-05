using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace API.Services
{
    public class CacheRedisService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _options;

        public CacheRedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(120)
            };
        }

        public T Get<T>(string key)
        {
            var cache = _distributedCache.Get(key);

            if (cache is null) return default;

            var result = JsonSerializer.Deserialize<T>(cache);

            return result;
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }

        public void Set<T>(string key, T content)
        {
            var contentAsString = JsonSerializer.Serialize(content);
            _distributedCache.SetString(key, contentAsString, _options);
        }
    }
}
