using System.Data;
using Dapper;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Enums;
using LearnShop.Model.Sales;

namespace LearnShop.Infra;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<IEnumerable<Order?>> GetAllAsync()
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
        SELECT
            o.id, o.customer_id as CustomerId, o.total_price as TotalPrice,
            o.order_status as Status, o.created_at as CreatedAt,
            oi.id, oi.order_id as OrderId, oi.ebook_id as EbookId,
            oi.quantity, oi.unit_price as UnitPrice, oi.created_at as CreatedAt
        FROM orders o
        INNER JOIN order_items oi ON o.id = oi.order_id";

            var orderDictionary = new Dictionary<long, Order>();

            var result = await connection.QueryAsync<Order, OrderItem, Order>(
                sql,
                (order, orderItem) =>
                {
                    if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.Items = new List<OrderItem>();
                        orderDictionary.Add(order.Id, orderEntry);
                    }

                    orderEntry.Items.Add(orderItem);
                    return orderEntry;
                },
                splitOn: "id"
            );

            return orderDictionary.Values;
        });
    }

    public async Task<Order?> GetByIdAsync(long id)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
            SELECT 
                o.id, o.customer_id as CustomerId, o.total_price as TotalPrice, 
                o.order_status as Status, o.created_at as CreatedAt,
                oi.id, oi.order_id as OrderId, oi.ebook_id as EbookId, 
                oi.quantity, oi.unit_price as UnitPrice, oi.created_at as CreatedAt
            FROM orders o
            INNER JOIN order_items oi ON o.id = oi.order_id
            WHERE o.id = @OrderId";

            var orderDictionary = new Dictionary<long, Order>();

            var result = await connection.QueryAsync<Order, OrderItem, Order>(
                sql,
                (order, orderItem) =>
                {
                    if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.Items = new List<OrderItem>();
                        orderDictionary.Add(order.Id, orderEntry);
                    }

                    orderEntry.Items.Add(orderItem);
                    return orderEntry;
                },
                new { OrderId = id },
                splitOn: "id" 
            );

            return result.FirstOrDefault();
        });
    }

    public async Task<Order?> InsertAsync(Order order)
    {   
        return await ExecuteWithConnectionAsync(async connection =>
        {   
            using var transaction = connection.BeginTransaction();
            try
            { 
                const string orderSql = @"
                    INSERT INTO orders(customer_id, total_price,order_status,created_at)
                    VALUES (@CustomerId, @TotalPrice, @OrderStatus, @CreatedAt)
                    RETURNING id, customer_id AS CustomerId, total_price AS TotalPrice, order_status AS OrderStatus, created_at AS CreatedAt";
                
                string orderStatusValue = order.Status == OrderStatus.Concluido ? "concluido" : "pendente";
                
                var insertedOrder = await connection.QueryFirstOrDefaultAsync<Order>(
                    orderSql,
                    new
                    {
                        order.CustomerId,
                        order.TotalPrice,
                        OrderStatus = orderStatusValue,
                        CreatedAt = order.CreatedAt.ToString("yyyy-MM-dd HH:mm")
                    },
                    transaction);

                if (insertedOrder == null)
                {
                    throw new Exception("Erro ao inserir o pedido.");
                }
                
                const string orderItemSql = @"
                    INSERT INTO order_items(order_id, ebook_id, quantity, unit_price, created_at)
                    VALUES (@OrderId, @EbookId, @Quantity, @UnitPrice, @CreatedAt)";

                foreach (var item in order.Items)
                {
                    await connection.ExecuteAsync(
                        orderItemSql,
                        new
                        {
                            OrderId = insertedOrder.Id,
                            EbookId = item.EbookId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            CreatedAt = item.CreatedAt.ToString("yyyy-MM-dd HH:mm")
                        },
                        transaction);
                }
                
                var completedOrder = await connection.QueryFirstOrDefaultAsync<Order>(
                    "SELECT * FROM orders WHERE Id = @Id",
                    new { Id = insertedOrder.Id },
                    transaction);
                
                transaction.Commit();
                return completedOrder;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine($"Erro detalhado: {e.Message}");
                if (e.InnerException != null) Console.WriteLine($"Inner: {e.InnerException.Message}");
                throw new Exception("Erro ao inserir o pedido e os itens do pedido.", e);
            }
        });
    }

    public async Task<Order?> UpdateAsync(long id, Order order)
{
    return await ExecuteWithConnectionAsync(async connection =>
    {
        using var transaction = connection.BeginTransaction();
        try
        {
            const string orderSql = @"
                UPDATE orders
                SET customer_id = @CustomerId,
                    total_price = @TotalPrice,
                    order_status = @OrderStatus,
                    created_at = @CreatedAt
                WHERE Id = @Id;";

            await connection.ExecuteAsync(orderSql, new
            {
                Id = id,
                CustomerId = order.CustomerId,
                TotalPrice = order.TotalPrice,
                OrderStatus = order.Status.ToString(),
                CreatedAt = order.CreatedAt
            }, transaction);
            
            const string deleteItemsSql = "DELETE FROM order_items WHERE order_id = @OrderId";
            await connection.ExecuteAsync(deleteItemsSql, new { OrderId = id }, transaction);
            
            const string insertItemSql = @"
                INSERT INTO order_items(order_id, ebook_id, quantity, unit_price, created_at)
                VALUES (@OrderId, @EbookId, @Quantity, @UnitPrice, @CreatedAt)";
            foreach (var item in order.Items)
            {
                await connection.ExecuteAsync(insertItemSql, new
                {
                    OrderId = id,
                    EbookId = item.EbookId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    CreatedAt = item.CreatedAt.ToString("yyyy-MM-dd HH:mm")
                }, transaction);
            }
            
            var updatedOrder = await connection.QueryFirstOrDefaultAsync<Order>(
                "SELECT * FROM orders WHERE Id = @Id",
                new { Id = id },
                transaction);

            transaction.Commit();
            return updatedOrder;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new Exception("Erro ao atualizar o pedido e os itens.", e);
        }
    });
}

    public async Task DeleteAsync(long id)
    {
        await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "DELETE FROM orders WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        });
    }
}