using OrderService.Application.Models;

namespace OrderService.Application.Contracts;

public interface IRabbitMQPublisherService : IAsyncDisposable
{
    Task PublishNotificaitionRequestAsync(NotificationMessage message);
    Task PublishStockUpdateAsync(StockUpdateMessage message);

    ValueTask DisposeAsync();
}