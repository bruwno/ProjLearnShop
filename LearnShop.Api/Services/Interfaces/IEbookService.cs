using LearnShop.Model.Products;

namespace LearnShop.Api.Services.Interfaces;

public interface IEbookService
{
    Task<List<Ebook>> GetEbooksAsync();
    Task<Ebook?> GetEbookByIdAsync(long id);
    Task<List<Ebook>> GetEbooksByCategoryAsync(string category);
    Task<Ebook> CreateEbookAsync(Ebook ebook);
}