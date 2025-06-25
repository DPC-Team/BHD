namespace BHD.Application.Common.Interfaces.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        T Get<T>(string key, Func<T> getValue);
        Task<T> GetAsync<T>(string key);
        Task<T> GetAsync<T>(string key, Func<T> getValue);
        T Set<T>(string key, T value);
        Task<T> SetAsync<T>(string key, T value);
    }
}
