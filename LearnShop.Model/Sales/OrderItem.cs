namespace LearnShop.Model.Sales;

public class OrderItem : BaseModel
{
    public long OrderId { get; set; }
    public long EbookId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}