namespace NotificationService.Common.Models.Requests;

public class EmailDto
{
    public string To { get; set; }
    public string Subject { get; set; }

    public string Body { get; set; }
}