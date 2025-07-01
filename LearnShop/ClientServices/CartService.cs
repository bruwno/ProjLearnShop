using System.Text.Json;
using LearnShop.ClientServices.Interfaces;
using LearnShop.Model.Products;
using LearnShop.Model.Sales;
using Microsoft.JSInterop;

namespace LearnShop.ClientServices;

public class CartService : ICartService
{   
    private readonly IJSRuntime _jsRuntime;
    private const string CartStorageKey = "learnshop_cart";
    
    public event Action? OnCartChanged;
    
    public CartService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        try
        {
            var cartJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CartStorageKey);
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();
                
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao carregar carrinho: {ex.Message}", ex);
        }
    }
    
    public async Task<int> GetCartItemsCountAsync()
    {
       var cartItems = await GetCartItemsAsync();
       return cartItems.Sum(item => item.Quantity);
    }

    public async Task<decimal> GetCartTotalPriceAsync()
    {
       var cartItems = await GetCartItemsAsync();
       return cartItems.Sum(item => item.Price * item.Quantity);
    }

    public async Task AddToCartAsync(Ebook ebook, int quantity)
    {
        var cartItems = await GetCartItemsAsync();
        var existingItem = cartItems.FirstOrDefault(item => item.EbookId == ebook.Id);
        
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cartItems.Add(new CartItem
            {
                EbookId = ebook.Id,
                Title = ebook.Title,
                ImageUrl = ebook.ImageUrl,
                Price = ebook.Price,
                Quantity = quantity
            });
        }
        
        await SaveCartAsync(cartItems);
        OnCartChanged?.Invoke();
    }
    public async Task RemoveFromCartAsync(long ebookId)
    {
        var cartItems = await GetCartItemsAsync();
        var itemToRemove = cartItems.FirstOrDefault(item => item.EbookId == ebookId);
        
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
            await SaveCartAsync(cartItems);
            OnCartChanged?.Invoke();
        }
    }
    
    public async Task UpdateCartItemQuantityAsync(long ebookId, int quantity)
    {
        var cartItems = await GetCartItemsAsync();
        var item = cartItems.FirstOrDefault(item => item.EbookId == ebookId);
        
        if (item != null)
        {
            if (quantity <= 0)
            {
                await RemoveFromCartAsync(ebookId);
            }
            else
            {
                item.Quantity = quantity;
                await SaveCartAsync(cartItems);
                OnCartChanged?.Invoke();
            }
        }
    }
    
    public async Task ClearCartAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", CartStorageKey);
        OnCartChanged?.Invoke();
    }
    private async Task SaveCartAsync(List<CartItem> cartItems)
    {
        try
        {
            var cartJson = JsonSerializer.Serialize(cartItems);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CartStorageKey, cartJson);
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao salvar carrinho: {ex.Message}", ex);
        }
    }
}