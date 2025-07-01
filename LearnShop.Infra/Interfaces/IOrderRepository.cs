using LearnShop.Model.Sales;

namespace LearnShop.Infra.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomerIdAsync(long customerId);
}