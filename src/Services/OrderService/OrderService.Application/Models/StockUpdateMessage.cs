namespace OrderService.Application.Models;

public class StockUpdateMessage
{
    public Guid OrderId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}