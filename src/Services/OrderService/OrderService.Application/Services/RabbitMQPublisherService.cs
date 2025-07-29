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

    private async Task PublishMessage<T>(T message, string queueName, string routingKey)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _appSettingsModel.QueueConfiguration.HostName,
            Port = _appSettingsModel.QueueConfiguration.Port,
            UserName = _appSettingsModel.QueueConfiguration.UserName,
            Password = _appSettingsModel.QueueConfiguration.Password
        };

        IConnection? _connection = await factory.CreateConnectionAsync();
        IChannel _channel = await _connection.CreateChannelAsync();

        if (_channel == null)
            throw new RabbitMQException("RabbitMQ channel is not created. Call ConnectAsync first.");

        await _channel.ExchangeDeclareAsync(_appSettingsModel.QueueConfiguration.ExchangeName, ExchangeType.Direct);

        await _channel.QueueDeclareAsync(queueName, true, false, false, null);
        await _channel.QueueBindAsync(queueName, _appSettingsModel.QueueConfiguration.ExchangeName, routingKey);

        var properties = new BasicProperties();
        properties.Persistent = true; //Mesaj kalıcı olsun diye.

        string jsonString = JsonConvert.SerializeObject(message);
        byte[] body = Encoding.UTF8.GetBytes(jsonString);

        await _channel.BasicPublishAsync(_appSettingsModel.QueueConfiguration.ExchangeName, routingKey, true, properties, body);
    }

    public async Task PublishStockUpdateAsync(StockUpdateMessage message)
    {
        await PublishMessage<StockUpdateMessage>(message, _appSettingsModel.QueueConfiguration.StockQueueName, _appSettingsModel.QueueConfiguration.StockRoutingKey);
    }

    public async Task PublishNotificaitionRequestAsync(NotificationMessage message)
    {
        await PublishMessage<NotificationMessage>(message, _appSettingsModel.QueueConfiguration.NotificationQueueName, _appSettingsModel.QueueConfiguration.NotificationRoutingKey);
    }
}