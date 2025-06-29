using LearnShop.Model.Products;
using LearnShop.Model.Users;

namespace LearnShop.Model.Sales;

public class Order : BaseModel
{
    public long CustomerId { get; set; }
    public OrderItem OrderItem { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public decimal TotalPrice { get; set; } = 0.0m;
}