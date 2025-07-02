using System.Text.Json;
using DotNetEnv;
using LearnShop.Api.Configs.Cors;
using LearnShop.Api.Configs.SQLite;
using LearnShop.Api.Configs.Authentication;
using LearnShop.Api.Endpoints;
using LearnShop.Api.Services;
using LearnShop.Api.Services.Interfaces;
using LearnShop.Infra;
using LearnShop.Infra.Interfaces;
using Scalar.AspNetCore;

namespace LearnShop.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load();
        
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.ConfigureCors();
        builder.Services.AddJwtConfiguration();
        
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
        });
        
        // Registrando as dependências
        builder.Services.AddSqliteConfiguration();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddScoped<ISaleService, SaleService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IEbookRepository, EbookRepository>();
        builder.Services.AddScoped<IEbookService, EbookService>();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        
        app.UseHttpsRedirection();
        app.UseCors("AllowAllOrigins");
        app.UseAuthentication();
        app.UseAuthorization();
        app.RegisterAllEndpoints();
        
        app.Run();
    }
}