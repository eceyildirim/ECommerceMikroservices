using Microsoft.EntityFrameworkCore;
using StockService.Domain.Entities;

namespace StockService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<StockReservation> StockReservations => Set<StockReservation>();
    public DbSet<StockReservationItem> StockReservationItems => Set<StockReservationItem>();
    // public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

}