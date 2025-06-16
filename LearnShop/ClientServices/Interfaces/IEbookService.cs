using LearnShop.Model.Products;

namespace LearnShop.ClientServices.Interfaces;

public interface IEbookService
{
    Task<List<Ebook>> GetEbooksAsync();
    Task<List<Ebook>> GetEbooksByCategoryAsync(string category);
}