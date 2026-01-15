using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Blazored.LocalStorage;
using SentinelIoT;
using SentinelIoT.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Подключаем UI и сервисы
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<SensorService>(); // Наш Mock-сервис данных

await builder.Build().RunAsync();
