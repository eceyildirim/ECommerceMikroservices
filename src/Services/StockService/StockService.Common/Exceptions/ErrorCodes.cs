namespace StockService.Common.Exceptions;

public static class ErrorCodes
{
    public const string DefaultException = "ERR_STCK_000";
    public const string VALIDATION_ERROR_CODE = "ERR_STCK_001";
    public const string DB_ERROR_CODE = "ERR_STCK_002";
    public const string RABBITMQ_ERROR_CODE = "ERR_STCK_003";
    public const string STOCK_ERROR_CODE = "ERR_STCK_004";
    public const string STOCK_NOT_FOUND_CODE = "ERR_STCK_005";
}