using Azure.Core.Extensions;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using MyFurniture;
using MyFurniture.Components;
using MyFurniture.Redis;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOpenTelemetry().UseAzureMonitor();

builder
    .Services
    .Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));

builder
    .Services
    .AddBlazorBootstrap()
    .AddHttpClient()
    .AddMemoryCache();

/*
 * redis custom impl
 * require nuget Microsoft.Azure.StackExchangeRedis
 */
builder
    .Services
    // inject connection string using IOptions pattern
    .Configure<RedisCacheSettings>(builder.Configuration.GetSection(nameof(RedisCacheSettings)))
    .AddSingleton<IRedisCache, RedisCache>();

/*
 * redis through IDistributedCache impl
 * require nugets
 *      Microsoft.Azure.StackExchangeRedis
 *      Microsoft.Extensions.Caching.StackExchangeRedis
 */
var redisCs = builder.Configuration.GetSection(nameof(RedisCacheSettings))["ConnectionString"];

builder.Services
    .AddStackExchangeRedisCache(options => { options.Configuration = redisCs; })
    // this will make a connection multiplexer object available for IDistributedCache implementation
    .AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisCs));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();