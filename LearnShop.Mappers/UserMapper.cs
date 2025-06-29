using LearnShop.Dto.RequestDtos;
using LearnShop.Dto.ResponseDtos;
using LearnShop.Model.Users;

namespace LearnShop.Mappers;

public class UserMapper
{
    public static User ToEntity(UserCreateRequestDto userCreateRequestDto)
    {
        User user = new User
        {
            FullName = userCreateRequestDto.FullName,
            Cpf = userCreateRequestDto.Cpf,
            Email = userCreateRequestDto.Email,
            Role = userCreateRequestDto.Role
        };

        user.SetPassword(userCreateRequestDto.PasswordHash);
        return user;
    }
    
    public static void UpdateEntity(User user, UserUpdateRequestDto userUpdateRequestDto)
    {
        user.FullName = userUpdateRequestDto.FullName ?? user.FullName;
        user.Cpf = userUpdateRequestDto.Cpf ?? user.Cpf;
        user.Email = userUpdateRequestDto.Email ?? user.Email;
        user.Role = userUpdateRequestDto.Role ?? user.Role;

        if (!string.IsNullOrWhiteSpace(userUpdateRequestDto.Password))
        {
            user.SetPassword(userUpdateRequestDto.Password);
        }
    }
    
    public static UserResponseDto ToResponseDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }
}