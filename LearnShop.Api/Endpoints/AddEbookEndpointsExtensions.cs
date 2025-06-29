using LearnShop.Api.Services.Interfaces;
using LearnShop.Model.Products;

namespace LearnShop.Api.Endpoints;

public static class AddEbookEndpointsExtensions
{
    public static void AddEbookEndpoints(this WebApplication app)
    {
        var ebooks = app.MapGroup("/ebooks");

        ebooks.MapPost("/", CreateEbook);
        ebooks.MapGet("/{id:long}", GetEbookById);
    }

    private static async Task<IResult> CreateEbook(Ebook ebook, IEbookService ebookService)
    {
        try
        {
            var created = await ebookService.CreateEbookAsync(ebook);
            return TypedResults.Created($"/ebooks/{created?.Id}", created);
        }
        catch (ArgumentNullException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }

    private static async Task<IResult> GetEbookById(long id, IEbookService ebookService)
    {
        try
        {
            var ebook = await ebookService.GetEbookByIdAsync(id);
            if (ebook == null)
            {
                return TypedResults.NotFound($"Ebook com ID {id} não encontrado.");
            }

            return TypedResults.Ok(ebook);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
}

