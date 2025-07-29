namespace OrderService.Common.Exceptions;

///<summary>
/// Veritabanı işlemlerinde hata alındığında fırlatılacak exception sınıfı
/// </summary>
public class DBOperationException : Exception
{
    public DBOperationException() { }
    public DBOperationException(string message) : base(message) { }
    public DBOperationException(string message, Exception exp) : base(message, exp) { }
}