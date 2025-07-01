using System.Net.Http.Json;
using LearnShop.ClientServices.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.ClientServices;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Order>>("data/orders.json") ?? new List<Order>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao tentar obter os pedidos: {ex.Message}");
            return new List<Order>();
        }
    }
    
    public async Task<Order?> CreateOrderAsync(Order order)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("orders", order);
        
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Order>();
            }
        
            Console.WriteLine($"Erro ao criar pedido: {response.StatusCode} - {response.ReasonPhrase}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao tentar criar o pedido: {ex.Message}");
            return null;
        }
    }
}