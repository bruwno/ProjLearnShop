using LearnShop.Api.Services.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Api.Endpoints;

public static class AddSaleEndpointsExtensions
{
    public static void AddSaleEndpoints(this WebApplication app)
    {
        var sales = app.MapGroup("/sales");

        sales.MapGet("/", GetAllSales).RequireAuthorization(policy => policy.RequireRole("Admin"));
        sales.MapGet("/{id:long}", GetSaleById).RequireAuthorization(policy => policy.RequireRole("Admin"));
        sales.MapPost("/", InsertSale).RequireAuthorization();
        sales.MapPut("/{id:long}", UpdateSale).RequireAuthorization(policy => policy.RequireRole("Admin"));
        sales.MapDelete("/{id:long}", DeleteSale)
            .RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
    
    private static async Task<IResult> GetAllSales(ISaleService saleService)
    {
        try
        {
            var sales = await saleService.GetAllAsync();
            return TypedResults.Ok(sales);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
    
    private static async Task<IResult> GetSaleById(long id, ISaleService saleService)
    {
        try
        {
            var sale = await saleService.GetByIdAsync(id);
            return sale is not null ? TypedResults.Ok(sale) : TypedResults.NotFound($"Venda com ID {id} não encontrada.");
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
    
    private static async Task<IResult> InsertSale(Sale sale, ISaleService saleService)
    {
        try
        {
            var insertedSale = await saleService.InsertAsync(sale);
            return TypedResults.Created($"/sales/{insertedSale.Id}", insertedSale);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
    
    private static async Task<IResult> UpdateSale(long id, Sale sale, ISaleService saleService)
    {
        try
        {
            var updatedSale = await saleService.UpdateAsync(id, sale);
            return updatedSale is not null ? TypedResults.Ok(updatedSale) : TypedResults.NotFound($"Venda com ID {id} não encontrada.");
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
    
    private static async Task<IResult> DeleteSale(long id, ISaleService saleService)
    {
        try
        {
            await saleService.DeleteAsync(id);
            return TypedResults.NoContent();
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
}