namespace StockService.Domain.Enums;

public enum StockTransactionType
{
    Order = 1, //sipariş alındı
    Cancel = 2, //sipariş iptali
    Add = 3, //sisteme stok girildi.
    Delete = 4 //sistemden stok çıkarıldı
}