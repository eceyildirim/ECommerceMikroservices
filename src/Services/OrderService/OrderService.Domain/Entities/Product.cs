namespace OrderService.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}