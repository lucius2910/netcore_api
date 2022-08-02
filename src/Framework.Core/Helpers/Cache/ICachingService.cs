namespace Framework.Core.Helpers.Cache
{
    public interface ICachingService
    {
        List<string> GetKeys();
        T Get<T>(string key, Func<T> fallback);
        Task<T> GetAsync<T>(string key, Func<Task<T>> fallback);
        void Clear(string key);
        Task ClearAsync(string key);
        Task ClearAsync(string[] keys);
        Task ClearAllAsync();
    }
}
