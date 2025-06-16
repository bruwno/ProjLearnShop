using LearnShop.Api.Services.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Api.Services;

public class OrderService : IOrderService
{
    public Task<List<Order>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }
}