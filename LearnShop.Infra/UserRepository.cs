using System.Data;
using Dapper;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Users;

namespace LearnShop.Infra;

public class UserRepository : BaseRepository, IUserRepository
{ 
    public UserRepository(IDbConnection dbConnection) : base(dbConnection)
    {
    }

    public async Task<IEnumerable<User?>> GetAllAsync()
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "SELECT * FROM Users";
            return await connection.QueryAsync<User>(sql);
        });
    }

    public Task<User?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "SELECT * FROM Users WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        });
    }
    
    public Task<User?> InsertAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User?> UpdateAsync(long id, User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}