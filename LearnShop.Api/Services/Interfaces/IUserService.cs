using LearnShop.Dto.RequestDtos;
using LearnShop.Dto.ResponseDtos;
using LearnShop.Model.Users;

namespace LearnShop.Api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(long id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<UserResponseDto?> CreateUserAsync(UserCreateRequestDto user);
    Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateRequestDto user);
    Task DeleteUserAsync(long id);
}