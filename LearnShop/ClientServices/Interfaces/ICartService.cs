using LearnShop.Dto.RequestDtos;
using LearnShop.Model.Products;
using LearnShop.Model.Sales;

namespace LearnShop.ClientServices.Interfaces;

public interface ICartService
{
   event Action OnCartChanged;
   Task<List<CartItem>> GetCartItemsAsync();
   Task<int> GetCartItemsCountAsync();
   Task<decimal> GetCartTotalPriceAsync();
   Task AddToCartAsync(Ebook ebook, int quantity);
   Task RemoveFromCartAsync(long ebookId);
   Task UpdateCartItemQuantityAsync(long ebookId, int quantity);
   Task ClearCartAsync();
}