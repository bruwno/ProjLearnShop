using System.Data.Common;
using System.Reflection;
using Dapper;
using LearnShop.Infra.Interfaces;
using LearnShop.Model;

namespace LearnShop.Infra;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : IModel
{
    protected readonly DbConnection _dbConnection;
    protected abstract string TableName { get; }

    public BaseRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public virtual async Task<IEnumerable<T?>> GetAllAsync()
    {
        var fields = "";

        foreach (var propName in GetProperties(typeof(T)))
        {
            fields += $", {propName.ToLower()} as {propName}";
        }

        string sql = "SELECT id as Id{fields}" +
                     " FROM {TableName}";

        return await _dbConnection.QueryAsync<T>(sql);
    }

    public virtual async Task<T?> GetByIdAsync(long id)
    {
        var fields = "";

        foreach (var propName in GetProperties(typeof(T)))
        {
            fields += $", {propName.ToLower()} as {propName}";
        }

        string sql = $"SELECT i as Id{fields}" +
                     $" FROM {TableName}" +
                     " WHERE id = @Id";

        return await SelectSingleAsync(sql, new { Id = id });
    }

    public virtual async Task InsertAsync(T obj)
    {
        string fieldNames = "";
        string parametersNames = "";

        foreach (var propName in GetProperties(obj))
        {
            if (!propName.ToLower().Equals("id"))
            {
                fieldNames += $", {propName.ToLower()}";
                parametersNames += $", @{propName}";
            }
        }

        string sql = $"INSERT INTO {TableName} " +
                     $" (id{fieldNames}" +
                     " VALUES " +
                     $" (@Id{parametersNames})";

        await ExecuteAsync(sql, obj);
    }

    public virtual async Task UpdateAsync(T obj)
    {
        var comma = "";
        var fields = "";

        foreach (var propName in GetProperties(obj))
        {
            if (!propName.ToLower().Equals("id"))
            {
                fields += $"{comma}{propName.ToLower()} = @{propName}";
                comma = ", ";
            }
        }

        string sql = $"UPDATE {TableName}" +
                     " SET {fields}" +
                     " WHERE id " +
                     " id = @Id";

        await ExecuteAsync(sql, obj);
    }

    public virtual async Task DeleteAsync(long id)
    {
        string sql = $"DELETE FROM {TableName} WHERE id = @Id";
        await ExecuteAsync(sql, new { Id = id });
    }

    protected async Task ExecuteAsync(string sql, object obj)
    {
        using var connection = _dbConnection;
        await connection.OpenAsync();
        await connection.ExecuteAsync(sql, obj);
        await connection.CloseAsync();
    }
    
    protected async Task<IEnumerable<T>> SelectAsync(string sql, object? obj = null)
    {
        if (obj == null)
            return await _dbConnection.QueryAsync<T>(sql);

        return await _dbConnection.QueryAsync<T>(sql, obj);
    }

    protected async Task<T?> SelectSingleAsync(string sql, object? parameters = null)
    {
        if (parameters == null)
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql);
        
        return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, parameters);
    }

    protected async Task<K?> SelectSingleAsync<K>(string sql, object? parameters = null)
    {
        if (parameters == null)
            return await _dbConnection.QuerySingleOrDefaultAsync<K>(sql);

        return await _dbConnection.QuerySingleOrDefaultAsync<K>(sql, parameters);
    }
    
    protected IEnumerable<string> GetProperties(T obj)
    {
        return GetProperties(obj.GetType());
    }

    protected IEnumerable<string> GetProperties(Type type)
    {
        return type.GetProperties()
            .Where(x => !x.Name.Equals("Id"))
            .Select(x => x.Name);
    }
}