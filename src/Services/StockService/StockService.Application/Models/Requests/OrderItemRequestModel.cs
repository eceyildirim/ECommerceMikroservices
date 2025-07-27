namespace StockService.Application.Models.Requests;

public class OrderItemRequestModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}