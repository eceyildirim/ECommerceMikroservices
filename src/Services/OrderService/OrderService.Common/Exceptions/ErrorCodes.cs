namespace OrderService.Common.Exceptions;

public static class ErrorCodes
{
    public const string DefaultException = "ERR_ORD_000";
    public const string VALIDATION_ERROR_CODE = "ERR_ORD_001";
    public const string DB_ERROR_CODE = "ERR_ORD_002";
    public const string RABBITMQ_ERROR_CODE = "ERR_ORD_003";
}