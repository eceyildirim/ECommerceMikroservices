using Microsoft.EntityFrameworkCore;
using StockService.Domain.Entities;

namespace StockService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Stock> Stocks => Set<Stock>();
    // public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Sku)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.BasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.QuantityAvailable)
                .IsRequired();

            // Relationship: Stock -> Product (many-to-one)
            entity.HasOne(s => s.Product)
                .WithMany()  // Product tarafında collection yok
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StockTransaction>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.Property(e => e.Type)
                .IsRequired();

            entity.HasOne(st => st.Stock)
                .WithMany(s => s.Transactions)
                .HasForeignKey(st => st.StockId)
                .OnDelete(DeleteBehavior.Cascade);

        });

    }

}