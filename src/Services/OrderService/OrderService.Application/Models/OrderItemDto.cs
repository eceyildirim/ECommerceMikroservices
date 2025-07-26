namespace OrderService.Application.Models;

public class OrderItemDto : BaseDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}