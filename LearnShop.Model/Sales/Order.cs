using LearnShop.Model.Products;
using LearnShop.Model.Users;

namespace LearnShop.Model.Sales;

public class Order : BaseModel
{
    public Customer Customer { get; set; }
    public List<Ebook> Items { get; set; } = new List<Ebook>();
    public int Number { get; set; }
    public DateTime Date { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string DownloadUrl { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; } = 0.0m;
}