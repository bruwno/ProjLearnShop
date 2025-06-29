using LearnShop.Api.Services.Interfaces;
using LearnShop.Dto.RequestDtos;
using LearnShop.Dto.ResponseDtos;
using LearnShop.Mappers;
using LearnShop.Model.Users;

namespace LearnShop.Api.Endpoints;

public static class AddUserEndpointsExtensions
{
    public static void AddUserEndpoints(this WebApplication app)
    {
        var users = app.MapGroup("/users");

        users.MapGet("/email/{email}", GetUserByEmail);
        users.MapPost("/login", Login);
        users.MapPost("/register", RegisterUser);
        users.MapPut("/{id:long}", UpdateUser);
        users.MapGet("/", GetAllUsers);
        users.MapGet("/{id:long}", GetUserById);
        users.MapPost("/users/{id:long}/delete", DeleteUser);
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

    private static async Task<IResult> RegisterUser(UserCreateRequestDto usercreateRequestDto, IUserService userService){
        try
        {
            if (usercreateRequestDto == null)
            {
                return TypedResults.BadRequest("O usuário não pode ser nulo.");
            }
            
            var createdUser = await userService.CreateUserAsync(usercreateRequestDto);

            if (createdUser == null)
            {
                return TypedResults.Conflict("O usuário já existe.");
            }

            return TypedResults.Created($"/users/{createdUser.Id}", createdUser);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }

    }
    
    private static async Task<IResult> UpdateUser(long id, UserUpdateRequestDto userUpdateRequestDto, IUserService userService)
    {
        try
        {
            if (userUpdateRequestDto == null)
            {
                return TypedResults.BadRequest("O corpo da requisição não pode ser nulo.");
            }

            var updatedUser = await userService.UpdateUserAsync(id, userUpdateRequestDto);

            if (updatedUser == null)
            {
                return TypedResults.NotFound("Usuário não encontrado.");
            }

            return TypedResults.Ok(updatedUser);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
    
    private static async Task<IEnumerable<UserResponseDto>> GetAllUsers(IUserService userService)
    {
        try
        {
            var users = await userService.GetAllUsersAsync();
            return users.Select(UserMapper.ToResponseDto);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocorreu um erro ao obter os usuários: {ex.Message}");
        }
    }
    
    private static async Task<IResult> GetUserById(long id, IUserService userService)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return TypedResults.NotFound($"Usuário com ID {id} não encontrado.");
            }

            return TypedResults.Ok(UserMapper.ToResponseDto(user));
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
    
    private static async Task<IResult> DeleteUser(long id, IUserService userService)
    {
        try
        {
            await userService.DeleteUserAsync(id);
            return TypedResults.NoContent();
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
}