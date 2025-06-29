using LearnShop.Api.Services.Interfaces;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Api.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<Sale?>> GetAllAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale?> GetByIdAsync(long id)
    {
        return await _saleRepository.GetByIdAsync(id);
    }

    public async Task<Sale?> InsertAsync(Sale sale)
    {
        return await _saleRepository.InsertAsync(sale);
    }

    public async Task<Sale?> UpdateAsync(long id, Sale sale)
    {
        return await _saleRepository.UpdateAsync(id, sale);
    }

    public async Task DeleteAsync(long id)
    {
        await _saleRepository.DeleteAsync(id);
    }
}