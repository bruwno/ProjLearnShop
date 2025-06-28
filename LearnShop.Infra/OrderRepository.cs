using System.Data;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Infra;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IDbConnection connection) : base(connection)
    {
    }

    public Task<IEnumerable<Order?>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> InsertAsync(Order entity)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> UpdateAsync(long id, Order entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}