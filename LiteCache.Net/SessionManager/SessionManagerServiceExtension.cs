namespace LiteCache.Net.SessionManager;

public static class SessionManagerServiceExtension
{
    public static void AddSessionManagerService(this IServiceCollection service)
    {
        service.AddSingleton<ISessionManager, SessionManager>();
    }
}