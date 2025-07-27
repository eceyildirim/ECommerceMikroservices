namespace NotificationService.Application.Models.Requests;

public class SMSDto
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
}