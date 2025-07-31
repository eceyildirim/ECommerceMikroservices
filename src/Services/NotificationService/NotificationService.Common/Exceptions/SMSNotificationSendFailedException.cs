namespace NotificationService.Common.Exceptions;

public class SMSNotificationSendFailedException : Exception
{
    public int ErrorCode { get; }

    public SMSNotificationSendFailedException() { }
    public SMSNotificationSendFailedException(string message) : base(message) { }

    public SMSNotificationSendFailedException(int errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}