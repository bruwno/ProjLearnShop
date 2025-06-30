using System.Net.Http.Json;
using LearnShop.ClientServices.Interfaces;
using LearnShop.Dto.RequestDtos;
using LearnShop.Dto.ResponseDtos;

namespace LearnShop.ClientServices;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<UserResponseDto?> RegisterUserAsync(UserCreateRequestDto userCreateRequestDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5057/users/register", userCreateRequestDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponseDto>();
            }

            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Falha ao registrar usuário: {error}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao registrar usuário: {ex.Message}", ex);
        }
    }
}