using System.Data;
using Microsoft.Data.Sqlite;

namespace LearnShop.Api.Configs.SQLite;

public static class SqliteConnectionConfig
{
    public static string ConnectionString { get; private set; } = string.Empty;

    public static void ConfigureSqlite(IServiceCollection services)
    {
        var baseConnectionString = Environment.GetEnvironmentVariable("SQLITE_CONNECTION_STRING");
        if (string.IsNullOrEmpty(baseConnectionString))
        {
            throw new InvalidOperationException("A variável de ambiente 'SQLITE_CONNECTION_STRING' não está definida.");
        }
        
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(baseDir, "Database", "learnshop.db");
        ConnectionString = baseConnectionString.Replace("Data Source=learnshop.db", $"Data Source={dbPath}");

        services.AddScoped<IDbConnection>(serviceProvider =>
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        });
    }
}