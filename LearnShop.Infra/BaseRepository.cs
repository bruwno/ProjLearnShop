using System.Data;


namespace LearnShop.Infra;

public abstract class BaseRepository
{
    protected readonly IDbConnection _connection;

    protected BaseRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    protected async Task<T> ExecuteWithConnectionAsync<T>(Func<IDbConnection, Task<T>> action)
    {
        return await action(_connection);
    }

    protected async Task ExecuteWithConnectionAsync(Func<IDbConnection, Task> action)
    {
        await action(_connection);
    }
}