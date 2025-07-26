using OrderService.Domain.Enums;

namespace OrderService.Application.Models;

public class OrderDto : BaseDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public CustomerDto Customer { get; set; }
    public AddressDto ShippingAddress { get; set; }
    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}