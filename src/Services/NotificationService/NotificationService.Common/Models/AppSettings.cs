namespace NotificationService.Common.Models;

public class AppSettings
{
    public EmailSettings EmailSettings { get; set; }
    public Twilio Twilio { get; set; }
}

public class Twilio
{
    public string AccountSid { get; set; }
    public string AuthToken { get; set; }
    public string FromPhoneNumber { get; set; }

}
public class EmailSettings
{
    public string From { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string SmtpServer { get; set; }
}
