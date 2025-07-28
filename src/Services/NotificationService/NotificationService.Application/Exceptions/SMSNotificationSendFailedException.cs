namespace NotificationService.Application.Exceptions;

public class SMSNotificationSendFailedException : Exception
{
    public int ErrorCode { get; }

    public SMSNotificationSendFailedException() { }

    public SMSNotificationSendFailedException(int errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}