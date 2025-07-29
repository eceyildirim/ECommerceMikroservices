namespace OrderService.Common;

public class AppSettingsModel
{
    public QueueConfiguration QueueConfiguration { get; set; }
}

public class QueueConfiguration
{
    public string ExchangeName { get; set; }
    public string HostName { get; set; }
    public string StockQueueName { get; set; }
    public string NotificationQueueName { get; set; }
    public string StockRoutingKey { get; set; }
    public string NotificationRoutingKey { get; set; }
}