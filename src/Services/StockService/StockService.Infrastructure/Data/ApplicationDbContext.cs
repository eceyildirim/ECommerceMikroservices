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

        // Ürün oluştur
        var productList = SeedProduct();
        modelBuilder.Entity<Product>().HasData(productList);

    }

    private List<Product> SeedProduct()
    {
        var productNames = new List<string>
        {
            "Laptop",
            "Akıllı Telefon",
            "Tablet",
            "Bluetooth Kulaklık",
            "Akıllı Saat",
            "Harici Disk",
            "Kamera",
            "Oyun Konsolu",
            "SSD Disk",
            "Kablosuz Mouse",
            "Akıllı Saat",
            "Laptop Çantası"
        };

        var random = new Random(2025);
        var products = new List<Product>();

        for (int i = 0; i < productNames.Count; i++)
        {
            var product = productNames[i];
            decimal price = (decimal)(random.Next(100000, 1500001)) / 100m;

            string description = $"Ürün Bilgisi: {product} Fiyatı: {price} TL.";

            products.Add(new Product
            {
                Id = Guid.Parse($"3027ccfd-d16f-4209-8846-0000000000{i + 1:D2}"),
                Name = product,
                BasePrice = price,
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            });
        }

        return products;
    }

}