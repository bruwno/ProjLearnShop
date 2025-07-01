using LearnShop.Model.Sales;

namespace LearnShop.ClientServices.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order?> CreateOrderAsync(Order order);
    Task<List<Order>> GetOrdersByCustomerIdAsync(long customerId);
}