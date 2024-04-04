using BlazorWebAppOidc.Client;
using BlazorWebAppOidc.Client.Weather;
using BlazorWebAppOidc.Services.Http;
using BlazorWebAppOidc.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddHttpClient<IWeatherForecaster, ClientWeatherForecaster>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddScoped<AuthorizationMessageHandler>();
builder.Services.AddHttpClient<IArticleService, ArticleService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7166/");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

await builder.Build().RunAsync();