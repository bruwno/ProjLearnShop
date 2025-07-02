using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LearnShop;
using LearnShop.ClientServices;
using LearnShop.ClientServices.Auth;
using LearnShop.ClientServices.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IEbookService, EbookService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IEbookService, EbookService>();

await builder.Build().RunAsync();
