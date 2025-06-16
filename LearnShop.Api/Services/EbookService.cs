using LearnShop.Api.Services.Interfaces;
using LearnShop.Model.Products;

namespace LearnShop.Api.Services;

public class EbookService : IEbookService
{
    public Task<List<Ebook>> GetEbooksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Ebook>> GetEbooksByCategoryAsync(string category)
    {
        throw new NotImplementedException();
    }
}