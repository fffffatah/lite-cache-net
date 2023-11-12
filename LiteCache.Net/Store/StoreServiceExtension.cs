namespace LiteCache.Net.Store;

public static class StoreServiceExtension
{
    public static IServiceCollection AddStoreService(this IServiceCollection service)
    {
        service.AddSingleton<IStoreService, StoreService>();

        return service;
    }
}