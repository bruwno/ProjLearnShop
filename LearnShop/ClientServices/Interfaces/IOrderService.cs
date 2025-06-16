using LearnShop.Model.Sales;

namespace LearnShop.ClientServices.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync();
}