namespace NotificationService.Application.Models.Requests;

public class OrderSuccessSMSRequestModel
{
    public string PhoneNumber { get; set; }
    public Order Order { get; set; }
}