namespace StockService.Messaging;

public class StockUpdateMessage
{
    public Guid OrderId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}