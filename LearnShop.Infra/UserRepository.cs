using System.Data;
using System.Data.Common;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Users;

namespace LearnShop.Infra;

public class UserRepository : BaseRepository<User>, IUserRepository
{ 
    protected override string TableName => "users";

    public UserRepository(DbConnection dbConnection) : base(dbConnection)
    {
    }
    
    public Task<IEnumerable<User?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
    
    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(Guid id, User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public Task InsertAsync(User obj)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User obj)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}