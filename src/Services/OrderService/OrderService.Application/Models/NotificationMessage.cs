using OrderService.Application.Models;
namespace OrderService.Application.Models;

public class NotificationMessage
{
    //Email, SMS
    public List<ChannelType> ChannelTypes { get; set; }
    public OrderDto Order { get; set; }

}

public enum ChannelType
{
    Email = 1,
    SMS = 2
}