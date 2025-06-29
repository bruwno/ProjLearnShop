using LearnShop.Api.Services.Interfaces;
using LearnShop.Infra.Interfaces;
using LearnShop.Mappers;
using LearnShop.Dto.RequestDtos;
using LearnShop.Dto.ResponseDtos;
using LearnShop.Model.Users;

namespace LearnShop.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return _userRepository.GetAllAsync();
    }

    public Task<User> GetUserByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }
        
        return await _userRepository.GetUserByEmail(email);
    }

    public async Task<UserResponseDto> CreateUserAsync(UserCreateRequestDto usercreateRequestDto)
    {
        var userRequestDto = UserMapper.ToEntity(usercreateRequestDto);
        var createdUser = await _userRepository.InsertAsync(userRequestDto);
        var userResponseDto = UserMapper.ToResponseDto(createdUser);
        return userResponseDto;
    }

    public async Task<UserResponseDto> UpdateUserAsync(long id, UserUpdateRequestDto userUpdateRequestDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if(existingUser == null)
            throw new Exception("Usuário não encontrado.");
        
        var updatedUser = await _userRepository.UpdateAsync(id, existingUser);
        return UserMapper.ToResponseDto(updatedUser);
    }

    public Task DeleteUserAsync(long id)
    {
        throw new NotImplementedException();
    }
}