using LearnShop.Api.Services.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Api.Endpoints;

public static class AddOrderEndpointsExtensions
{
    public static void AddOrderEndpoints(this WebApplication app)
    {
        var orders = app.MapGroup("/orders");
        
        orders.MapGet("/", GetAllOrders);
        orders.MapGet("/{id:long}", GetOrderById);
        orders.MapPost("/", CreateOrder);
        orders.MapPut("/{id:long}", UpdateOrder);
        orders.MapDelete("/{id:long}", DeleteOrder);
    }

    private static async Task<IResult> GetAllOrders(IOrderService orderService)
    {
        try
        {
            var orders = await orderService.GetOrdersAsync();
            return TypedResults.Ok(orders);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }

    private static async Task<IResult> GetOrderById(long id, IOrderService orderService)
    {
        try
        {
            var order = await orderService.GetOrderByIdAsync(id);
            
            if (order == null)
            {
                return TypedResults.NotFound($"O pedido com ID {id} não foi encontrado.");
            }
            
            return TypedResults.Ok(order);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }

    private static async Task<IResult> CreateOrder(Order order, IOrderService orderService)
    {
        try
        {
            var newOrder = await orderService.CreateOrderAsync(order);
            return TypedResults.Created($"/orders/{newOrder.Id}", newOrder);
        }
        catch (ArgumentNullException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }

    private static async Task<IResult> UpdateOrder(long id, Order order, IOrderService orderService)
    {
        try
        {
            var updatedOrder = await orderService.UpdateOrderAsync(id, order);
            return TypedResults.Ok(updatedOrder);
        }
        catch (ArgumentNullException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }

    private static async Task<IResult> DeleteOrder(long id, IOrderService orderService)
    {
        try
        {
            await orderService.DeleteOrderAsync(id);
            return TypedResults.NoContent();
        }
        catch (ArgumentException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return TypedResults.InternalServerError($"Ocorreu um erro interno: {ex.Message}");
        }
    }
}