using System.Data;
using Microsoft.Data.Sqlite;

namespace LearnShop.Api.Configs.SQLite;

public static class SqliteConnectionConfig
{
    public static string ConnectionString { get; private set; } = string.Empty;

    public static IServiceCollection AddSqliteConfiguration(this IServiceCollection services)
    {
        var baseConnectionString = Environment.GetEnvironmentVariable("SQLITE_CONNECTION_STRING");
        if (string.IsNullOrEmpty(baseConnectionString))
        {
            throw new InvalidOperationException("A variável de ambiente 'SQLITE_CONNECTION_STRING' não está definida.");
        }
        
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(baseDir, "Database", "learnshop.db");
        ConnectionString = baseConnectionString.Replace("Data Source=Database/learnshop.db;", $"Data Source={dbPath};");
        
        var directory = Path.GetDirectoryName(dbPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        services.AddScoped<IDbConnection>(serviceProvider =>
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        });

        return services; 
    }
}