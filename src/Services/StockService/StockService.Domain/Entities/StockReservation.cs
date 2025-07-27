using StockService.Domain.Enums;

namespace StockService.Domain.Entities;

public class StockReservation
{
    public Guid Id { get; set; }                 // PK
    public Guid OrderId { get; set; }            // Rezervasyon Order bazlı
    public DateTime ReservedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsCompleted { get; set; }        // OrderCompleted geldi mi?
    public ReservationStatus Status { get; set; } // Reserved, Completed, Expired, Cancelled
    public ICollection<StockReservationItem> Items { get; set; } = new List<StockReservationItem>();
}