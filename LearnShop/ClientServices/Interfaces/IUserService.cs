using LearnShop.Dto.RequestDtos;
using LearnShop.Dto.ResponseDtos;

namespace LearnShop.ClientServices.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> RegisterUserAsync(UserCreateRequestDto userCreateRequestDto);
}