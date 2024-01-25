using System.ComponentModel.DataAnnotations;
using LiteCache.Net;
using LiteCache.Net.Constants;
using LiteCache.Net.SessionManager;
using LiteCache.Net.Store;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false);
builder.Configuration.GetSection("CacheConfigs").Bind(new Configs());

builder.Services.AddStoreService();
//builder.Services.AddSessionManagerService();

var app = builder.Build();

app.MapPost("/set", async ([FromBody, Required]RequestBody requestBody, IStoreService storeService) =>
{
    if (requestBody.Key is null || requestBody.Value is null)
        return Results.BadRequest(ResponseConstants.ValidationError);
        
    return await storeService.SetAsync(requestBody.Key, requestBody.Value) ? Results.Ok(ResponseConstants.Ok) : Results.BadRequest(ResponseConstants.Error);
})
.Produces(200);

app.MapGet("/get/{key}", async ([FromRoute, Required]string? key, IStoreService storeService) =>
{
    if (key is null)
        return Results.BadRequest(ResponseConstants.ValidationError);
    
    var response = await storeService.GetAsync(key);

    return (!string.IsNullOrEmpty(response)) ? Results.Ok(response) : Results.BadRequest(ResponseConstants.Error);
})
.Produces(200);

app.MapPut("/update/{key}", async ([FromBody, Required]RequestBody requestBody, [FromRoute, Required]string? key, IStoreService storeService) =>
{
    if (requestBody.Key is null || requestBody.Value is null || key is null)
        return Results.BadRequest(ResponseConstants.ValidationError);
    
    var oldValue = await storeService.GetAsync(requestBody.Key);
    var response = await storeService.UpdateAsync(key, oldValue, requestBody.Value);

    return response ? Results.Ok(ResponseConstants.Ok) : Results.BadRequest(ResponseConstants.Error);
})
.Produces(200);
    

app.MapGet("/exists/{key}", async ([FromRoute, Required] string? key, IStoreService storeService) =>
    {
        if (key is null)
            return Results.BadRequest(ResponseConstants.ValidationError);
        
        return await storeService.ExistsAsync(key) ? Results.Ok(true) : Results.NotFound(false);
    })
.Produces(200);

app.MapDelete("/delete/{key}", async ([FromRoute, Required] string? key, IStoreService storeService) =>
    {
        if (key is null)
            return Results.BadRequest(ResponseConstants.ValidationError);
        
        return await storeService.DeleteAsync(key) ? Results.Ok(ResponseConstants.Ok) : Results.BadRequest(ResponseConstants.Error);
    })
.Produces(200);

app.Run();