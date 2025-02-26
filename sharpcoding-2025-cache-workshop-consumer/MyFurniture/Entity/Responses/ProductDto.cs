namespace MyFurniture.Entity.Responses;

public class ProductDto
{
    public required string Id { get; set; } 
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
}