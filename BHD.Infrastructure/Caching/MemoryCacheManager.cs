using BHD.Application.Common.Interfaces.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace BHD.Infrastructure.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() => Get<T>(key));
        }

        public async Task<T> GetAsync<T>(string key, Func<T> getValue)
        {
            var value = await GetAsync<T>(key);
            if (value != null)
                return value;

            value = getValue();
            if (value != null)
            {
                await SetAsync(key, value);
                return value;
            }

            return default!;
        }

        public T Get<T>(string key, Func<T> getValue)
        {
            var value = Get<T>(key);
            if (value != null)
                return value;

            value = getValue();

            if (value != null)
            {
                Set(key, value);
                return value;
            }

            return default!;
        }

        public T Set<T>(string key, T value)
        {
            return _cache.Set(key, value);
        }

        public Task<T> SetAsync<T>(string key, T value)
        {
            return Task.Run(() => _cache.Set(key, value));
        }
    }
}
