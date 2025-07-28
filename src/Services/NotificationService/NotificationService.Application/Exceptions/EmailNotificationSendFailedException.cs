namespace NotificationService.Application.Exceptions;

public class EmailNotificationSendFailedException : Exception
{
    public int ErrorCode { get; }

    public EmailNotificationSendFailedException(int errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}