namespace OrderService.Application.Models;

public class ProductDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}