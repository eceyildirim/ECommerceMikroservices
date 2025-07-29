namespace OrderService.Common.Exceptions;

///<summary>
/// RabbitMQ işlemlerinde hata alındığında fırlatılacak exception sınıfı
/// </summary>
public class RabbitMQException : Exception
{
    public RabbitMQException() { }
    public RabbitMQException(string message) : base(message) { }
    public RabbitMQException(string message, Exception exp) : base(message, exp) { }
}