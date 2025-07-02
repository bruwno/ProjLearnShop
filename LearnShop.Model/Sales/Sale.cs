using LearnShop.Model.Users;

namespace LearnShop.Model.Sales;

public class Sale : BaseModel
{
    public long OrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime SaleDate { get; set; }
}