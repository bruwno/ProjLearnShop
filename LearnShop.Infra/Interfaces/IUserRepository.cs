using LearnShop.Model.Users;

namespace LearnShop.Infra.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmail(string email);
}