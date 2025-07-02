using System.Text.Json.Serialization;

namespace LearnShop.Model.Sales;

public class CartItem
{
    [JsonPropertyName("ebookId")] 
    public long EbookId { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    
}