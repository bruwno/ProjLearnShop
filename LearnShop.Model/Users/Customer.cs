using LearnShop.Model.Sales;

namespace LearnShop.Model.Users;

public class Customer : User
{
    public List<Order> Orders { get; set; } = new List<Order>();
   
}