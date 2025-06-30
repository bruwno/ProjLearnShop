using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LearnShop.Model.Enums;
using LearnShop.Model.Users;
using Microsoft.JSInterop;

namespace LearnShop.ClientServices.Auth;

public class AuthenticationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    public event Action? OnAuthenticationStateChanged;
    
    public AuthenticationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<UserInfo?> GetCurrentUserAsync()
    {
        try
        {
            var token = await GetTokenAsync();
            
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            if (!_tokenHandler.CanReadToken(token))
            {
                return null;
            }

            var jwtToken = _tokenHandler.ReadJwtToken(token);
            
            if (jwtToken.ValidTo <= DateTime.Now)
            {
                await LogoutAsync();
                return null;
            }

            return DecodeToken(jwtToken);
        }
        catch (Exception ex)
        {
            await LogoutAsync();
            return null;
        }
    }

    public async Task SaveTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", token);
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "token");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "user");
        OnAuthenticationStateChanged?.Invoke();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var user = await GetCurrentUserAsync();
        return user is not null;
    }

    public async Task<string?> GetUserIdAsync()
    {
        var user = await GetCurrentUserAsync();
        return user?.UserId.ToString();
    }

    public async Task<string?> GetUsernameAsync()
    {
        var user = await GetCurrentUserAsync();
        return user?.Username;
    }
    
    public async Task<string?> GetUserEmailAsync()
    {
        var user = await GetCurrentUserAsync();
        return user?.Email;
    }

    public async Task<Role?> GetUserRoleAsync()
    {
        var user = await GetCurrentUserAsync();
        return user?.Role;
    }

    private static UserInfo? DecodeToken(JwtSecurityToken token)
    {
        try
        {
            var userId = token.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var fullName = token.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            var email = token.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            var role = token.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
            {
                return null;
            }

            return new UserInfo
            {
                UserId = long.Parse(userId),
                FullName = fullName,
                Email = email,
                Role = Enum.TryParse<Role>(role, out var parsedRole) ? parsedRole : Role.Cliente
            };
        }
        catch
        {
            return null;
        }
    }
}