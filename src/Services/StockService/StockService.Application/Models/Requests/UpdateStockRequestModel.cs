namespace StockService.Application.Models.Requests;

public class UpdateStockRequestModel
{
    public Guid OrderId { get; set; }
    public List<OrderItemRequestModel> Items { get; set; } = new List<OrderItemRequestModel>();
}