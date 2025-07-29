namespace StockService.WorkerService.Enums;

public enum OrderStatus
{
    Pending = 0,        // Sipariş alındı, işlem bekleniyor
    Completed = 1,     // Sipariş hazırlanıyor
    Cancelled = 2,     // Müşteri siparişi iptal etti
    OperationalCancelled = 3 // Sipariş sistem tarafından stok yetersizliğinden iptal edildi
}