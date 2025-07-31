namespace NotificationService.Common.Models;
using NotificationService.Common.Enums;

public class NotificationLogDto : BaseEntityDto
{
    public string Receiver { get; set; } //Email ya da Telefon no
    public string ChannelType { get; set; }//SMS, Email
    public Guid TransactionId { get; set; }
    public string LogType { get; set; } //Request, Response
    public string JsonValue { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime LogDate { get; set; }
}