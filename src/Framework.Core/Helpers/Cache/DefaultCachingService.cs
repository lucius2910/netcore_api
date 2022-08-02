using Microsoft.Extensions.Caching.Memory;

namespace Framework.Core.Helpers.Cache
{
    public class DefaultCachingService : ICachingService
    {
        private readonly IMemoryCache _cache;
        private const int _timeToLive = 7;

        public DefaultCachingService(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public void Clear(string key)
        {
            _cache.Remove(key);

        }

        public Task ClearAllAsync()
        {
            if (_cache is MemoryCache memoryCache)
            {
                var percentage = 1.0;//100%
                memoryCache.Compact(percentage);
            }
            return Task.CompletedTask;
        }

        public Task ClearAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task ClearAsync(string[] keys)
        {
            foreach (var item in keys)
            {
                _cache.Remove(item);
            }
            return Task.CompletedTask;
        }

        public T Get<T>(string key, Func<T> fallback)
        {
            bool foundInCache = _cache.TryGetValue(key, out T value);
            if (foundInCache) return value;
            value = fallback();
            if(value != null)
                _cache.Set(key, value, TimeSpan.FromDays(_timeToLive));
            return value;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> fallback)
        {
            return await _cache.GetOrCreateAsync(key, async m =>
            {
                return await Task.FromResult(await fallback());
            });
        }

        public List<string> GetKeys()
        {
            throw new NotImplementedException();
        }
    }
}
