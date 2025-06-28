using DotNetEnv;
using LearnShop.Api.Configs.Cors;
using LearnShop.Api.Configs.SQLite;
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
        string connectionString = Environment.GetEnvironmentVariable("SQLITE_CONNECTION_STRING")!;
        
        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.ConfigureCors();

        // Registrando a SQLite connection
        builder.Services.AddSqliteConfiguration();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.RegisterAllEndpoints();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        app.Run();
    }
}