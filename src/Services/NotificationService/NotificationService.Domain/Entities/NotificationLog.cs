using NotificationService.Domain.Enums;

namespace NotificationService.Domain.Entities;

public class NotificationLog : BaseEntity
{
    public string Receiver { get; set; } //Email ya da Telefon no
    public string ChannelType { get; set; } //SMS, Email
    public Guid TransactionId { get; set; } //İlgili 
    public string LogType { get; set; } //Request, Response
    public string JsonValue { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime LogDate { get; set; }
}