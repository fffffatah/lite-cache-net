namespace LiteCache.Net.SessionManager;

public static class SessionManagerServiceExtension
{
    public static IServiceCollection AddSessionManagerService(this IServiceCollection service)
    {
        service.AddSingleton<ISessionManager, SessionManager>();

        return service;
    }
}