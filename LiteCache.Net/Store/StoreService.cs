using System.Collections.Concurrent;

namespace LiteCache.Net.Store;

public class StoreService : IStoreService
{
    private readonly ConcurrentDictionary<string, string> _cache = new();

    public async Task<bool> SetAsync(string key, string value)
    {
        return await Task.Run(() =>
            _cache.TryAdd(key, value));
    }
    
    public async Task<bool> UpdateAsync(string key, string newValue, string oldValue)
    {
        return await Task.Run(() =>
            _cache.TryUpdate(key, newValue, oldValue));
    }

    public async Task<string?> GetAsync(string key)
    {
        return await Task.Run(() =>
        {
            var response = _cache.TryGetValue(key, out var value);
            
            return response ? value : String.Empty;
        });
    }

    public async Task<bool> DeleteAsync(string key)
    {
        return await Task.Run(() =>
            _cache.TryRemove(key, out _));
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return  await Task.Run(() =>
            _cache.ContainsKey(key));
    }
}