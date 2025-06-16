using System.Net.Http.Json;
using LearnShop.ClientServices.Interfaces;
using LearnShop.Model.Products;

namespace LearnShop.ClientServices;

public class EbookService : IEbookService
{
    private readonly HttpClient _httpClient;
    
    public EbookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Ebook>> GetEbooksAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Ebook>>("data/products.json") ?? new List<Ebook>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocorreu um erro ao tentar obter os ebooks: {ex.Message}");
        }
    }

    public async Task<List<Ebook>> GetEbooksByCategoryAsync(string category)
    {
        try
        {
            var ebooksFromCategory = await _httpClient.GetFromJsonAsync<List<Ebook>>("data/products.json");

            if (ebooksFromCategory == null || !ebooksFromCategory.Any())
            {
                return new List<Ebook>();
            }

            var cleanedCategory = category.Replace("-", " ", StringComparison.OrdinalIgnoreCase);

            var ebooks = ebooksFromCategory.Where(e => e.Category.Equals(cleanedCategory, StringComparison.OrdinalIgnoreCase)).ToList();

            return ebooks.Count > 0 ? ebooks : new List<Ebook>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocorreu um erro ao tentar obter os ebooks da categoria {category}: {ex.Message}");
        }
    }
}