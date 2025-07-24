namespace OrderService.Domain.Enums;

public enum OrderStatus
{
    Pending = 0,        // Sipariş alındı, işlem bekleniyor
    Processing = 1,     // Sipariş işleniyor
    Shipped = 2,        // Kargoya verildi
    Delivered = 3,      // Teslim edildi
    Cancelled = 4,      // Müşteri iptal etti
    Failed = 5          // Ödeme veya işlem hatası
}