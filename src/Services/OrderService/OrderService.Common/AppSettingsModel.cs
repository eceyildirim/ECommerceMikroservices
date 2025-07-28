namespace OrderService.Common;

public class AppSettingsModel
{
    public StockQueueConfiguration StockQueueConfiguration { get; set; }
    public NotificationQueueConfiguration NotificationQueueConfiguration { get; set; }
}

public class StockQueueConfiguration
{
    public string HostName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ClientProvidedName { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
}

public class NotificationQueueConfiguration
{
    public string HostName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ClientProvidedName { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
}