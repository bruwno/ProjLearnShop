using System.Data;
using LearnShop.Model.Sales;

namespace LearnShop.Infra.Interfaces;

public class SaleRepository : BaseRepository, ISaleRepository
{
    
    public SaleRepository(IDbConnection connection) : base(connection)
    {
    }

    public Task<IEnumerable<Sale?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> InsertAsync(Sale entity)
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> UpdateAsync(Guid id, Sale entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}