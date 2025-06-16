using LearnShop.Model.Sales;

namespace LearnShop.Api.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync();
}