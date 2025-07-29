using System.Text;
using OrderService.Application.Contracts;
using OrderService.Application.Models;
using OrderService.Common;
using RabbitMQ.Client;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using OrderService.Common.Exceptions;

namespace OrderService.Application.Services;

public class RabbitMQPublisherService : IRabbitMQPublisherService
{
    private readonly AppSettingsModel _appSettingsModel;

    public RabbitMQPublisherService(IOptions<AppSettingsModel> appSettingsModel)
    {
        _appSettingsModel = appSettingsModel.Value;
    }

    private async Task PublishMessage<T>(T message, string queueName, string exchangeName, string routingKey)
    {
        var factory = new ConnectionFactory() { HostName = _appSettingsModel.QueueConfiguration.HostName };

        IConnection? _connection = await factory.CreateConnectionAsync();
        IChannel _channel = await _connection.CreateChannelAsync();

        if (_channel == null)
            throw new RabbitMQException("RabbitMQ channel is not created. Call ConnectAsync first.");

        await _channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

        await _channel.QueueDeclareAsync(queueName, true, false, false, null);
        await _channel.QueueBindAsync(queueName, exchangeName, routingKey);

        var properties = new BasicProperties();
        properties.Persistent = true; //Mesaj kalıcı olsun diye.

        string jsonString = JsonConvert.SerializeObject(message);
        byte[] body = Encoding.UTF8.GetBytes(jsonString);

        await _channel.BasicPublishAsync(exchangeName, routingKey, true, properties, body);
    }

    public async Task PublishStockUpdateAsync(StockUpdateMessage message)
    {
        await PublishMessage<StockUpdateMessage>(message, _appSettingsModel.QueueConfiguration.StockQueueName, _appSettingsModel.QueueConfiguration.ExchangeName, _appSettingsModel.QueueConfiguration.StockRoutingKey);
    }

    public async Task PublishNotificaitionRequestAsync(NotificationMessage message)
    {
        await PublishMessage<NotificationMessage>(message, _appSettingsModel.QueueConfiguration.NotificationQueueName, _appSettingsModel.QueueConfiguration.ExchangeName, _appSettingsModel.QueueConfiguration.NotificationRoutingKey);
    }
}