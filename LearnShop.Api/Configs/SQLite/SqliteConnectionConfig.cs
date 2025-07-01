using System.Data;
using Microsoft.Data.Sqlite;

namespace LearnShop.Api.Configs.SQLite;

public static class SqliteConnectionConfig
{
    public static string ConnectionString { get; private set; } = string.Empty;

    public static IServiceCollection AddSqliteConfiguration(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("SQLITE_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A variável de ambiente 'SQLITE_CONNECTION_STRING' não está definida.");
        }
        
        var dataSourceMatch = System.Text.RegularExpressions.Regex.Match(connectionString, @"Data Source=([^;]+)");
        if (dataSourceMatch.Success)
        {
            var relativePath = dataSourceMatch.Groups[1].Value;
            var fullPath = Path.GetFullPath(relativePath);
            connectionString = connectionString.Replace(relativePath, fullPath);
        }
        
        ConnectionString = connectionString;
        
        var dbPathMatch = System.Text.RegularExpressions.Regex.Match(connectionString, @"Data Source=([^;]+)");
        if (dbPathMatch.Success)
        {
            var dbPath = dbPathMatch.Groups[1].Value;
            var directory = Path.GetDirectoryName(dbPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }
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