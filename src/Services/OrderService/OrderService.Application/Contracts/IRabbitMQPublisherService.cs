using OrderService.Application.Models;
using System;

namespace OrderService.Application.Contracts;

public interface IRabbitMQPublisherService
{
    Task PublishNotificaitionRequestAsync(NotificationMessage message);
    Task PublishStockUpdateAsync(StockUpdateMessage message);
}