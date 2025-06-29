using LearnShop.Api.Services.Interfaces;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Api.Services;

public class OrderService : IOrderService
{   
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Where(o => o != null).ToList();
    }

    public async Task<Order?> GetOrderByIdAsync(long id)
    {
       return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "O pedido não pode ser nulo");
        }
          
        if (order.Customer == null)
        {
            throw new ArgumentException("O pedido deve ter um cliente associado", nameof(order));
        }
        
        if (order.Customer.Id <= 0)
        {
            throw new ArgumentException("O ID do cliente deve ser maior que zero", nameof(order));
        }
        
        if (order.Items == null || !order.Items.Any())
        {
            throw new ArgumentException("O pedido deve conter pelo menos um item", nameof(order));
        }
        
        if (order.TotalPrice <= 0)
        {
            throw new ArgumentException("O valor total do pedido deve ser maior que zero", nameof(order));
        }
        
        if (order.Date > DateTime.Now)
        {
            throw new ArgumentException("A data do pedido não pode ser futura", nameof(order));
        }
        
        if (string.IsNullOrWhiteSpace(order.ProductName))
        {
            throw new ArgumentException("O nome do produto não pode estar vazio", nameof(order));
        }
        
        if (string.IsNullOrWhiteSpace(order.DownloadUrl))
        {
            throw new ArgumentException("A URL de download não pode estar vazia", nameof(order));
        }
        
        return await _orderRepository.InsertAsync(order);
    }

    public async Task<Order> UpdateOrderAsync(long id, Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "O pedido não pode ser nulo");
        }
        
        if (order.Id <= 0)
        {
            throw new ArgumentException("O ID do pedido deve ser maior que zero para atualização", nameof(order));
        }
        
        if (order.Customer == null)
        {
            throw new ArgumentException("O pedido deve ter um cliente associado", nameof(order));
        }
        
        if (order.Customer.Id <= 0)
        {
            throw new ArgumentException("O ID do cliente deve ser maior que zero", nameof(order));
        }
        
        if (order.Items == null || !order.Items.Any())
        {
            throw new ArgumentException("O pedido deve conter pelo menos um item", nameof(order));
        }
        
        if (order.TotalPrice <= 0)
        {
            throw new ArgumentException("O valor total do pedido deve ser maior que zero", nameof(order));
        }
        
        if (order.Date > DateTime.Now)
        {
            throw new ArgumentException("A data do pedido não pode ser futura", nameof(order));
        }
        
        if (string.IsNullOrWhiteSpace(order.ProductName))
        {
            throw new ArgumentException("O nome do produto não pode estar vazio", nameof(order));
        }
        
        if (string.IsNullOrWhiteSpace(order.DownloadUrl))
        {
            throw new ArgumentException("A URL de download não pode estar vazia", nameof(order));
        }

        return await _orderRepository.UpdateAsync(id, order);
    }

    public async Task DeleteOrderAsync(long id)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
        {
            throw new ArgumentException($"Pedido com ID {id} não encontrado", nameof(id));
        }
        await _orderRepository.DeleteAsync(id);
    }
}