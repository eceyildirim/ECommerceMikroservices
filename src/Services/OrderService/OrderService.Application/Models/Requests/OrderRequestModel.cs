using OrderService.Common.Enums;

namespace OrderService.Application.Models.Requests;

public class OrderRequestModel
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ShippingAddressId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}