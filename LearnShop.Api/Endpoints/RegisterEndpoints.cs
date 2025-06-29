namespace LearnShop.Api.Endpoints;

/// <summary>
/// Método para adicionar todos os endpoints da API.
/// </summary>
public static class RegisterEndpoints
{
    public static void RegisterAllEndpoints(this WebApplication app)
    {
        app.AddUserEndpoints();
        app.AddSaleEndpoints();
        app.AddOrderEndpoints();
    }
}