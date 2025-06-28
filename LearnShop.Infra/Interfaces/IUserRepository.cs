using LearnShop.Model.Users;

namespace LearnShop.Infra.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(Guid id, User user);
    Task DeleteUserAsync(Guid id);
}