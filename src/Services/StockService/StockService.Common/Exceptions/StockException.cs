namespace StockService.Common.Exceptions;

public class StockException : Exception
{
    public StockException() { }
    public StockException(string message) : base(message) { }
    public StockException(string message, Exception exp) : base(message, exp) { }
}