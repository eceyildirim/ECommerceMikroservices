namespace StockService.WorkerService.Models;

public class AppSettings
{
    public QueueConfiguration QueueConfiguration { get; set; }
    public string StockServiceAPIUrl { get; set; }
    public string OrderServiceAPIUrl { get; set; }
}

public class QueueConfiguration
{
    public string ExchangeName { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
    public string StockQueueName { get; set; } = string.Empty;
    public string StockRoutingKey { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}