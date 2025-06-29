using System.Data;
using Dapper;
using LearnShop.Infra.Interfaces;
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
            const string sql = "SELECT * FROM orders";
            return await connection.QueryAsync<Order>(sql);
        });
    }

    public async Task<Order?> GetByIdAsync(long id)
    {
        return await ExecuteWithConnectionAsync(async connection =>{
            const string sql = "SELECT * FROM orders WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Order>(sql, new { Id = id});
        });
    }

    public async Task<Order?> InsertAsync(Order order)
    {   
        return await ExecuteWithConnectionAsync(async connection =>
        {
           const string sql = @"
            INSERT INTO orders(customer_id, number, date, productName, downloadUrl, total_price)
            VALUES (@CustomerId, @Number, @Date, @ProductName, @DownloadUrl, @TotalPrice);
            SELECT * FROM orders WHERE Id = LAST_INSERT_ID();";

            var parameters = new
            {
                CustomerId = order.Customer?.Id ?? 0,
                order.Number,
                order.Date,
                order.ProductName,
                order.DownloadUrl,
                order.TotalPrice
            };

            return await connection.QueryFirstOrDefaultAsync<Order>(sql, parameters);
        });
    }

    public async Task<Order?> UpdateAsync(long id, Order order)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
            UPDATE orders
            SET customerId = @CustomerId, 
                number = @Number, 
                date = @Date, 
                productName = @ProductName, 
                downloadUrl = @DownloadUrl, 
                totalPrice = @TotalPrice
            WHERE Id = @Id;
            SELECT * FROM orders WHERE Id = @Id;";

            var parameters = new
            {
                Id = id,
                CustomerId = order.Customer?.Id ?? 0,
                order.Number,
                order.Date,
                order.ProductName,
                order.DownloadUrl,
                order.TotalPrice
            };

            return await connection.QueryFirstOrDefaultAsync<Order>(sql, parameters);
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