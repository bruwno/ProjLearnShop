using System.Data;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Infra;

public class SaleRepository : BaseRepository, ISaleRepository
{
    public SaleRepository(IDbConnection connection) : base(connection)
    {
    }

    public Task<IEnumerable<Sale?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> InsertAsync(Sale entity)
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> UpdateAsync(long id, Sale entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}