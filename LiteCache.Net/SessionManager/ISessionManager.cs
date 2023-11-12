namespace LiteCache.Net.SessionManager;

public interface ISessionManager
{
    Task<bool> Validate(string id);
    Task<string> Create();
}