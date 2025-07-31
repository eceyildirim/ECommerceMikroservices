using NotificationService.Common.Models;
using NotificationService.Common.Enums;
namespace NotificationService.WorkerService.Models;

public class NotificationMessage
{
    //Email, SMS
    public List<string> ChannelTypes { get; set; }
    public Order Order { get; set; }

}


