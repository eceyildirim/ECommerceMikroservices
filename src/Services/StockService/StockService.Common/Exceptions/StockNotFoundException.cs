namespace StockService.Common.Exceptions;

public class StockNotfoundException : Exception
{
    public StockNotfoundException() { }
    public StockNotfoundException(string message) : base(message) { }
    public StockNotfoundException(string message, Exception exp) : base(message, exp) { }
}