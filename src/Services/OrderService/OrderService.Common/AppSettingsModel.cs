namespace OrderService.Common;

public class AppSettingsModel
{
    public QueueConfiguration QueueConfiguration { get; set; }
}

public class QueueConfiguration
{
    public string ExchangeName { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public string StockQueueName { get; set; } = string.Empty;
    public string NotificationQueueName { get; set; } = string.Empty;
    public string StockRoutingKey { get; set; } = string.Empty;
    public string NotificationRoutingKey { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}