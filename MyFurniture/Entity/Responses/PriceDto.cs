namespace MyFurniture.Entity.Responses;

public class PriceDto
{
    public required string ProductId { get; set; }
    public required decimal Value { get; set; }
    public required string Currency { get; set; }
}