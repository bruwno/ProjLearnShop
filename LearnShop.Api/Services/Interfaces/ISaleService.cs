using LearnShop.Model.Sales;

namespace LearnShop.Api.Services.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<Sale?>> GetAllAsync();
    Task<Sale?> GetByIdAsync(long id);
    Task<Sale?> InsertAsync(Sale sale);
    Task<Sale?> UpdateAsync(long id, Sale sale);
    Task DeleteAsync(long id);
}