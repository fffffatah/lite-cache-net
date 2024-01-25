using LiteCache.Net;
using LiteCache.Net.SessionManager;
using LiteCache.Net.Store;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false);
builder.Configuration.GetSection("CacheConfigs").Bind(new Configs());

builder.Services.AddStoreService();
builder.Services.AddSessionManagerService();

var app = builder.Build();

app.MapPost("/set", async ([FromBody]RequestBody requestBody, IStoreService storeService) =>
{
    if (requestBody.Key is not null && requestBody.Value is not null)
        return Results.BadRequest();
        
    return await storeService.SetAsync(requestBody.Key, requestBody.Value) ? Results.Ok() : Results.BadRequest();
});

app.MapGet("/get/{key}", async (string key, IStoreService storeService) =>
{
    var response = await storeService.GetAsync(key);

    return (response is not null) ? Results.Ok(response) : Results.BadRequest();
});

app.MapGet("/update", async ([FromBody]RequestBody requestBody, string key, IStoreService storeService) =>
{
    var oldValue = await storeService.GetAsync(requestBody.Key);
    var response = await storeService.UpdateAsync(key, oldValue, requestBody.Value);

    return response ? Results.Ok() : Results.BadRequest();
});

app.MapGet("/exists/{key}", async (string key, IStoreService storeService) =>
    await storeService.ExistsAsync(key) ? Results.Ok() : Results.NotFound()
);

app.MapGet("/delete/{key}", async (string key, IStoreService storeService) =>
    await storeService.DeleteAsync(key) ? Results.Ok() : Results.BadRequest()
);

app.Run();