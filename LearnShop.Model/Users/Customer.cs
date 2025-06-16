using LearnShop.Model.Sales;

namespace LearnShop.Model.Users;

public class Customer : IModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<Order> Orders { get; set; } = new List<Order>();
}