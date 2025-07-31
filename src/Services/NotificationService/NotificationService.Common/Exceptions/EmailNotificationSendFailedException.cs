namespace NotificationService.Common.Exceptions;

public class EmailNotificationSendFailedException : Exception
{
    public int ErrorCode { get; }

    public EmailNotificationSendFailedException() { }
    public EmailNotificationSendFailedException(int errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}