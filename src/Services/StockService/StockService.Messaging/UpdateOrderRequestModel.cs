namespace StockService.Messaging;

public class UpdateOrderRequestModel
{
    public Guid OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}