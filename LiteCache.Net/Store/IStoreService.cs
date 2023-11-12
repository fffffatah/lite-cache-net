namespace LiteCache.Net.Store;

public interface IStoreService
{
    Task<bool> SetAsync(string key, string value);
    Task<bool> UpdateAsync(string key, string newValue, string oldValue);
    Task<string?> GetAsync(string key);
    Task<bool> DeleteAsync(string key);
    Task<bool> ExistsAsync(string key);
}