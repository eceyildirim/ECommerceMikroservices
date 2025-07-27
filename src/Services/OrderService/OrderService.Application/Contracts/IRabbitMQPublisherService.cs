using OrderService.Application.Models;

namespace OrderService.Application.Contracts;

public interface IRabbitMQPublisherService : IAsyncDisposable
{
    Task ConnectAsync();
    Task PublishStockUpdateAsync(StockUpdateMessage message);

    ValueTask DisposeAsync();
}