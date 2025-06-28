using LearnShop.Model.Sales;

namespace LearnShop.Model.Users;

public class Customer : User
{
    public List<Order> Orders { get; set; } = new List<Order>();
    public string? PhoneNumber { get; set; }
    public string FullName => $"{FirstName} {LastName}"; 
    public string Cpf { get; set; } = string.Empty;
}