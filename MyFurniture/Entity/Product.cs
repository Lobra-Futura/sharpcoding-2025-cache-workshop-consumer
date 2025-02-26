using MyFurniture.Entity.Responses;

namespace MyFurniture.Entity;

public class Product
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public Price? Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Stock { get; set; }
    public int PurchaseQuantity { get; set; }
}

public static class ProductMapper
{
    public static Product From(ProductDto dto, IReadOnlyList<AssetDto> assets,
        IReadOnlyList<PriceDto> prices, IReadOnlyList<StockDto> stocks)
    {
        var price = prices.FirstOrDefault(p => p.ProductId == dto.Id);
        
        return new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Category,
            Price = new Price
            {
                Value = price.Value,
                Currency = price.Currency
            },
            ImageUrl = assets.FirstOrDefault(a => a.ProductId == dto.Id)?.Url,
            Stock = stocks.FirstOrDefault(s => s.ProductId == dto.Id)?.Quantity ?? 0
        };
    }
}