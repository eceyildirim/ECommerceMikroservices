using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<NotificationLog> NotificationLogs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NotificationLog>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Receiver)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.ChannelType)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.TransactionId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.TransactionType)
                  .IsRequired();

            entity.Property(e => e.LogType)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.JsonValue)
                  .IsRequired();

            entity.Property(e => e.LogDate)
                  .IsRequired();
        });
    }
}