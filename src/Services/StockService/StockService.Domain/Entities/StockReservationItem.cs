namespace StockService.Domain.Entities;

public class StockReservationItem
{
    public Guid Id { get; set; }
    public Guid ReservationId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public StockReservation Reservation { get; set; }
    public Product Product { get; set; }
}