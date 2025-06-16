using LearnShop.Model.Products;

namespace LearnShop.Api.Services.Interfaces;

public interface IEbookService
{
    Task<List<Ebook>> GetEbooksAsync();
    Task<List<Ebook>> GetEbooksByCategoryAsync(string category);
}