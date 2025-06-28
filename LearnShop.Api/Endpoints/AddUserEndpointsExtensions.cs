using LearnShop.Api.Services.Interfaces;
using LearnShop.Dto.RequestDtos;

namespace LearnShop.Api.Endpoints;

public static class AddUserEndpointsExtensions
{
    public static void AddUserEndpoints(this WebApplication app)
    {
        var users = app.MapGroup("/users");
        
        users.MapGet("/email/{email}", GetUserByEmail);
        users.MapPost("/login", Login);
    }

    private static async Task<IResult> GetUserByEmail(string email, IUserService userService)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                return TypedResults.BadRequest("O campo e-mail não pode ficar vazio.");
            }
            
            var user = await userService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return TypedResults.NotFound($"O usuário com o e-mail '{email}' não foi encontrado.");
            }
            
            return TypedResults.Ok(user);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }

    private static async Task<IResult> Login(UserLoginRequestDto loginDto, IAuthService authService)
    {
        try
        {
            var token = await authService.AuthenticateAsync(loginDto);

            if (token is null)
            {
                return TypedResults.Unauthorized();
            }

            return TypedResults.Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
}