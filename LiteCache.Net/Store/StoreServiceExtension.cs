namespace LiteCache.Net.Store;

public static class StoreServiceExtension
{
    public static void AddStoreService(this IServiceCollection service)
    {
        service.AddSingleton<IStoreService, StoreService>();
    }
}