using LearnShop.Model.Users;

namespace LearnShop.Model.Sales;

public class Sale : BaseModel
{
    public long SaleId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime SaleDate { get; set; }
    public long CustomerId { get; set; }
}