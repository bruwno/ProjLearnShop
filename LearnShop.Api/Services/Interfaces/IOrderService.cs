using LearnShop.Model.Sales;

namespace LearnShop.Api.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order?> GetOrderByIdAsync(long id);
    Task<Order?> CreateOrderAsync(Order order);
    Task<Order?> UpdateOrderAsync(long id, Order order);
    Task DeleteOrderAsync(long id);
}