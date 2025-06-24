using LearnShop.Dto.RequestDtos;

namespace LearnShop.Api.Services.Interfaces;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(UserLoginRequestDto userLoginRequest);
}