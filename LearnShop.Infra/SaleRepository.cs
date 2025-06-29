using System.Data;
using Dapper;
using LearnShop.Infra.Interfaces;
using LearnShop.Model.Sales;

namespace LearnShop.Infra;

public class SaleRepository : BaseRepository, ISaleRepository
{
    public SaleRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<IEnumerable<Sale?>> GetAllAsync()
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"SELECT id, 
                                        order_id, 
                                        total_price, 
                                        sale_date
                                 FROM sales";

            return await connection.QueryAsync<Sale>(sql);
        });
    }

    public async Task<Sale?> GetByIdAsync(long id)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"SELECT id, 
                                        order_id, 
                                        total_price, 
                                        sale_date
                                FROM sales WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Sale>(sql, new { Id = id });
        });
    }

    public async Task<Sale?> InsertAsync(Sale sale)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
                INSERT INTO sales 
                    (id, order_id, total_price, sale_date)
                VALUES 
                    (@Id, @SaleId, @TotalPrice, @SaleDate)
                RETURNING
                    id, order_id, total_price, sale_date";

            var parameters = new
            {
                sale.Id,
                sale.OrderId,
                sale.TotalPrice,
                sale.SaleDate
            };

            var newSale = await connection.QuerySingleOrDefaultAsync<Sale>(sql, parameters);
            return newSale;
        });
    }

    public async Task<Sale?> UpdateAsync(long id, Sale sale)
    {
        return await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = @"
                UPDATE sales 
                SET order_id = @OrderId, 
                    total_price = @TotalPrice, 
                    sale_date = @SaleDate
                WHERE id = @Id
                RETURNING 
                    id, order_id, total_price, sale_date";

            var parameters = new
            {
                sale.OrderId,
                sale.TotalPrice,
                sale.SaleDate,
                Id = id
            };

            await connection.ExecuteAsync(sql, parameters);
            return sale;
        });
    }

    public async Task DeleteAsync(long id)
    {
        await ExecuteWithConnectionAsync(async connection =>
        {
            const string sql = "DELETE FROM sales WHERE id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        });
    }
}