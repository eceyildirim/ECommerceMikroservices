namespace OrderService.Common.Exceptions;

public static class ErrorMessages
{
    public const string DB_ERROR_MESSAGE = "Veritabanı işlemi sırasında bir hata oluştu.";
    public const string DefaultException = "Beklenmeyen bir hata oluştu.";
    public const string VALIDATION_ERROR_MESSAGE = "Giriş değerlerinde hatalı değerler mevcut.";
    public const string RABBITMQ_ERROR_MESSAGE = "RabbitMQ'da beklenmeyen bir hata oluştu.";
}