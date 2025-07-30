namespace NotificationService.WorkerService.Models;

public class AppSettings
{
    public QueueConfiguration QueueConfiguration { get; set; }
    public string StockServiceAPIUrl { get; set; }
    public string OrderServiceAPIUrl { get; set; }
    public string NotificationServiceAPIUrl { get; set; }
}

public class QueueConfiguration
{
    public string ExchangeName { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public string NotificationQueueName { get; set; } = string.Empty;
    public string NotificationRoutingKey { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}