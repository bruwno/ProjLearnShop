using System.Net.Http.Json;
using LearnShop.ClientServices.Interfaces;
using LearnShop.Helpers;
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

    // public async Task<LoginResponseDto?> LoginAsync(UserLoginRequestDto userLoginRequestDto)
    // {
    //     try
    //     {
    //         var response = await _httpClient.PostAsJsonAsync($"{ApiBackend.BaseUrl}/users/login", userLoginRequestDto);
    //         if (response.IsSuccessStatusCode)
    //         {
    //             return await response.Content.ReadFromJsonAsync<LoginResponseDto>();
    //         }
    //         
    //         var error = await response.Content.ReadAsStringAsync();
    //         throw new Exception($"Ocorreu uma falha na autenticação: {error}");
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception($"Erro ao autenticar: {e.Message}", e);
    //     }
    // }
}