using LearnShop.Api.Services.Interfaces;
using LearnShop.Model.Users;

namespace LearnShop.Api.Services;

public class UserService : IUserService
{
    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(Guid id)
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
}