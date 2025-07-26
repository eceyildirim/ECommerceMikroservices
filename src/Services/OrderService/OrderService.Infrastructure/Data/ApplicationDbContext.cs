using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Neighborhood> Neighborhoods { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(builder =>
        {
            builder.Property(o => o.CustomerId)
                .IsRequired();

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<int>(); // Enum int olarak saklanacak                    

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        });

        modelBuilder.Entity<Address>(builder =>
        {

            builder.Property(a => a.AddressLine)
               .HasMaxLength(500)
               .IsRequired();

            builder.Property(a => a.AddressLine2)
                .HasMaxLength(500);

            builder.HasOne(a => a.Customer)        
                .WithMany(c => c.Addresses)        
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);           

            builder.Property(a => a.PostalCode)
                .HasMaxLength(10);

            builder.HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .IsRequired();

            builder.HasOne(a => a.Province)
                .WithMany()
                .HasForeignKey(a => a.ProvinceId)
                .IsRequired();

            builder.HasOne(a => a.District)
                .WithMany()
                .HasForeignKey(a => a.DistrictId)
                .IsRequired();

            builder.HasOne(a => a.Neighborhood)
                .WithMany()
                .HasForeignKey(a => a.NeighborhoodId)
                .IsRequired();
        });

        modelBuilder.Entity<OrderItem>(builder =>
        {
            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // İlişkiler
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        });

        modelBuilder.Entity<Customer>(builder =>
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Surname)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Addresses)     
                .WithOne(a => a.Customer)          
                .HasForeignKey(a => a.CustomerId)  
                .IsRequired()                      
                .OnDelete(DeleteBehavior.Cascade); 
        });

        modelBuilder.Entity<Country>(builder =>
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(5);
        });

        modelBuilder.Entity<District>(builder =>
        {
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(d => d.Province)
                .WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Neighborhood>(builder =>
        {
            builder.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(n => n.District)
                .WithMany(d => d.Neighborhoods)
                .HasForeignKey(n => n.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Province>(builder =>
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(p => p.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        });

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
            new Country
            {
                Id = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                Name = "Türkiye",
                Code = "TR",
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false,
                UpdatedAt = null,
                DeletedAt = null
            }
        );

        modelBuilder.Entity<Province>().HasData(
            new Province
            {
                Id = Guid.Parse("0df01398-e42b-4f53-b00f-0e5a7a8978b2"),
                Name = "Adana",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("e921956a-48ed-47ec-99b5-b6ebe3b3b348"),
                Name = "Ankara",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("0bf70dca-87a1-4e40-a0c1-5b2dc6767afb"),
                Name = "Antalya",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("922e3d42-5970-48db-b164-bbba26ea1816"),
                Name = "Bursa",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("16599411-030b-495e-8a6c-6a3af09b1efc"),
                Name = "İzmir",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("cdc09180-6dd8-4b24-8ddd-b7b2ef463310"),
                Name = "Konya",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("56388bfa-007b-4742-a9c1-0bfc07766fa8"),
                Name = "Gaziantep",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("bcf25752-d7f4-4c90-a597-18b9371d4ddb"),
                Name = "Samsun",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("52415f74-3537-4ed9-a17b-ed31d0e18aea"),
                Name = "Trabzon",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Province
            {
                Id = Guid.Parse("7715dff3-417b-46d1-8085-c9e857a9589b"),
                Name = "Eskişehir",
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            }
        );

        modelBuilder.Entity<District>().HasData(
            // Adana
            new District
            {
                Id = Guid.Parse("9f147822-ff2f-4804-ae62-d58931f56d24"),
                Name = "Seyhan",
                ProvinceId = Guid.Parse("0df01398-e42b-4f53-b00f-0e5a7a8978b2"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("ef45957b-2e92-40a0-b8d8-5591c3538b56"),
                Name = "Çukurova",
                ProvinceId = Guid.Parse("0df01398-e42b-4f53-b00f-0e5a7a8978b2"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Ankara
            new District
            {
                Id = Guid.Parse("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"),
                Name = "Çankaya",
                ProvinceId = Guid.Parse("e921956a-48ed-47ec-99b5-b6ebe3b3b348"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("fa8a453d-0342-4a59-a356-1b7e1a335b52"),
                Name = "Keçiören",
                ProvinceId = Guid.Parse("e921956a-48ed-47ec-99b5-b6ebe3b3b348"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Antalya
            new District
            {
                Id = Guid.Parse("e89d05a3-77c3-4083-91f9-48a69220ef08"),
                Name = "Muratpaşa",
                ProvinceId = Guid.Parse("0bf70dca-87a1-4e40-a0c1-5b2dc6767afb"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("ab0e7e28-a602-43dd-b186-97551527a4cf"),
                Name = "Konyaaltı",
                ProvinceId = Guid.Parse("0bf70dca-87a1-4e40-a0c1-5b2dc6767afb"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Bursa
            new District
            {
                Id = Guid.Parse("4ca79d56-97f7-41e7-a921-81ae203b6a9f"),
                Name = "Osmangazi",
                ProvinceId = Guid.Parse("922e3d42-5970-48db-b164-bbba26ea1816"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("852d995d-6bdf-4336-b9ef-2ba939f51107"),
                Name = "Nilüfer",
                ProvinceId = Guid.Parse("922e3d42-5970-48db-b164-bbba26ea1816"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // İzmir
            new District
            {
                Id = Guid.Parse("5fad29e4-730b-43d2-8dcb-25800353d3b8"),
                Name = "Karşıyaka",
                ProvinceId = Guid.Parse("16599411-030b-495e-8a6c-6a3af09b1efc"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("432eac18-6f63-44e2-9f0a-7a9790a3cb7c"),
                Name = "Bornova",
                ProvinceId = Guid.Parse("16599411-030b-495e-8a6c-6a3af09b1efc"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Konya
            new District
            {
                Id = Guid.Parse("15b653d0-d69d-4e82-abbb-fc605b20bd88"),
                Name = "Selçuklu",
                ProvinceId = Guid.Parse("cdc09180-6dd8-4b24-8ddd-b7b2ef463310"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("0e76fb72-eeb1-454c-867d-d4b616cc20aa"),
                Name = "Meram",
                ProvinceId = Guid.Parse("cdc09180-6dd8-4b24-8ddd-b7b2ef463310"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Gaziantep
            new District
            {
                Id = Guid.Parse("a4da7b97-d21b-45ae-b5d9-eb8217ba4101"),
                Name = "Şahinbey",
                ProvinceId = Guid.Parse("56388bfa-007b-4742-a9c1-0bfc07766fa8"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("5455ae93-316f-4f09-b673-41b694961d30"),
                Name = "Şehitkamil",
                ProvinceId = Guid.Parse("56388bfa-007b-4742-a9c1-0bfc07766fa8"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Samsun
            new District
            {
                Id = Guid.Parse("6ed5a42a-2b61-49e0-8dae-68b108c158f6"),
                Name = "İlkadım",
                ProvinceId = Guid.Parse("bcf25752-d7f4-4c90-a597-18b9371d4ddb"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("89b9eac8-1a1c-415e-8662-55ea01c84871"),
                Name = "Atakum",
                ProvinceId = Guid.Parse("bcf25752-d7f4-4c90-a597-18b9371d4ddb"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Trabzon
            new District
            {
                Id = Guid.Parse("66373676-5a8a-4e3b-9eda-5ba02fdf3cf5"),
                Name = "Ortahisar",
                ProvinceId = Guid.Parse("52415f74-3537-4ed9-a17b-ed31d0e18aea"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f"),
                Name = "Akçaabat",
                ProvinceId = Guid.Parse("52415f74-3537-4ed9-a17b-ed31d0e18aea"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Eskişehir
            new District
            {
                Id = Guid.Parse("00e75515-6436-48bd-8062-08d0c24e6ac7"),
                Name = "Tepebaşı",
                ProvinceId = Guid.Parse("7715dff3-417b-46d1-8085-c9e857a9589b"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new District
            {
                Id = Guid.Parse("6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb"),
                Name = "Odunpazarı",
                ProvinceId = Guid.Parse("7715dff3-417b-46d1-8085-c9e857a9589b"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            }
        );

        modelBuilder.Entity<Neighborhood>().HasData(
            // Seyhan (DistrictId: 9f147822-ff2f-4804-ae62-d58931f56d24)
            new Neighborhood
            {
                Id = Guid.Parse("74662223-7112-4e09-a7b9-a4fea757d133"),
                Name = "Bahçe Mahallesi",
                DistrictId = Guid.Parse("9f147822-ff2f-4804-ae62-d58931f56d24"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("2e723ede-eba0-48dc-bacc-397ce65d95dd"),
                Name = "Toros Mahallesi",
                DistrictId = Guid.Parse("9f147822-ff2f-4804-ae62-d58931f56d24"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Çukurova (DistrictId: ef45957b-2e92-40a0-b8d8-5591c3538b56)
            new Neighborhood
            {
                Id = Guid.Parse("a88b21b4-2df1-40c2-bc92-1d216644b789"),
                Name = "Fettah Mahallesi",
                DistrictId = Guid.Parse("ef45957b-2e92-40a0-b8d8-5591c3538b56"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("4646c942-1d48-42cd-bf67-8827ae9edcb5"),
                Name = "Çay Mahallesi",
                DistrictId = Guid.Parse("ef45957b-2e92-40a0-b8d8-5591c3538b56"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Çankaya (DistrictId: ef8627cc-ad2b-4fa7-9450-4f570dd1f63e)
            new Neighborhood
            {
                Id = Guid.Parse("0b4cb6df-6bd8-4224-9608-3b99e7afe550"),
                Name = "Kızılay Mahallesi",
                DistrictId = Guid.Parse("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("f376d0eb-789c-43e4-862c-d4fbd2ad71e2"),
                Name = "Bahçelievler Mahallesi",
                DistrictId = Guid.Parse("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Keçiören (DistrictId: fa8a453d-0342-4a59-a356-1b7e1a335b52)
            new Neighborhood
            {
                Id = Guid.Parse("d00cdc4c-6924-4ea9-a220-d0a5953df310"),
                Name = "Etlik Mahallesi",
                DistrictId = Guid.Parse("fa8a453d-0342-4a59-a356-1b7e1a335b52"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("fbcb61da-c522-4820-a722-7a5eff1aeb9d"),
                Name = "Aşağı Eğlence Mahallesi",
                DistrictId = Guid.Parse("fa8a453d-0342-4a59-a356-1b7e1a335b52"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Muratpaşa (DistrictId: e89d05a3-77c3-4083-91f9-48a69220ef08)
            new Neighborhood
            {
                Id = Guid.Parse("d4a93ddc-5472-46a3-8327-d217948504dd"),
                Name = "Çağlayan Mahallesi",
                DistrictId = Guid.Parse("e89d05a3-77c3-4083-91f9-48a69220ef08"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("6a996b27-4df6-459a-8fbc-8f3216ea11bf"),
                Name = "Altınkum Mahallesi",
                DistrictId = Guid.Parse("e89d05a3-77c3-4083-91f9-48a69220ef08"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Konyaaltı (DistrictId: ab0e7e28-a602-43dd-b186-97551527a4cf)
            new Neighborhood
            {
                Id = Guid.Parse("4f05ea49-74b4-488c-87bc-4aa286e82b78"),
                Name = "Hurma Mahallesi",
                DistrictId = Guid.Parse("ab0e7e28-a602-43dd-b186-97551527a4cf"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("514ca6da-e656-4814-af7d-29a7220e5e85"),
                Name = "Gürsu Mahallesi",
                DistrictId = Guid.Parse("ab0e7e28-a602-43dd-b186-97551527a4cf"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Osmangazi (DistrictId: 4ca79d56-97f7-41e7-a921-81ae203b6a9ff)
            new Neighborhood
            {
                Id = Guid.Parse("29ab92d7-7586-413d-8041-a42b6f6372b5"),
                Name = "Beşevler Mahallesi",
                DistrictId = Guid.Parse("4ca79d56-97f7-41e7-a921-81ae203b6a9f"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("6477848e-7d2b-446d-adff-4c742b57be13"),
                Name = "Yıldırım Mahallesi",
                DistrictId = Guid.Parse("4ca79d56-97f7-41e7-a921-81ae203b6a9f"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Nilüfer (DistrictId: 852d995d-6bdf-4336-b9ef-2ba939f51107)
            new Neighborhood
            {
                Id = Guid.Parse("65942c42-a6b5-4af0-b63d-0a6e3d3df99e"),
                Name = "Ataevler Mahallesi",
                DistrictId = Guid.Parse("852d995d-6bdf-4336-b9ef-2ba939f51107"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("ab1872a3-a512-472d-98ea-81febc6bc7ba"),
                Name = "Nilüfer Mahallesi",
                DistrictId = Guid.Parse("852d995d-6bdf-4336-b9ef-2ba939f51107"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Selçuklu (DistrictId: 15b653d0-d69d-4e82-abbb-fc605b20bd88)
            new Neighborhood
            {
                Id = Guid.Parse("5ba61236-c1c0-4517-b2f8-a8a0fe679cda"),
                Name = "Fevzi Çakmak Mahallesi",
                DistrictId = Guid.Parse("15b653d0-d69d-4e82-abbb-fc605b20bd88"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("52ca3967-9b24-4878-82b0-242cee442647"),
                Name = "Bosna Hersek Mahallesi",
                DistrictId = Guid.Parse("15b653d0-d69d-4e82-abbb-fc605b20bd88"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Meram (DistrictId: 0e76fb72-eeb1-454c-867d-d4b616cc20aa)
            new Neighborhood
            {
                Id = Guid.Parse("248d4d1b-158b-4bfd-9064-3a0d70490a51"),
                Name = "Fatih Mahallesi",
                DistrictId = Guid.Parse("0e76fb72-eeb1-454c-867d-d4b616cc20aa"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("0f013f95-53ab-4f4c-b7c7-b79da2ca5cb5"),
                Name = "İstiklal Mahallesi",
                DistrictId = Guid.Parse("0e76fb72-eeb1-454c-867d-d4b616cc20aa"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Şahinbey (DistrictId: a4da7b97-d21b-45ae-b5d9-eb8217ba4101)
            new Neighborhood
            {
                Id = Guid.Parse("cd60fc90-b557-4682-b394-ca8461182da3"),
                Name = "Şehitler Mahallesi",
                DistrictId = Guid.Parse("a4da7b97-d21b-45ae-b5d9-eb8217ba4101"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("aa3a6fa6-7e6a-4a56-ae0e-14bd16e696fb"),
                Name = "Bahçelievler Mahallesi",
                DistrictId = Guid.Parse("a4da7b97-d21b-45ae-b5d9-eb8217ba4101"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Şehitkamil (DistrictId: 5455ae93-316f-4f09-b673-41b694961d30)
            new Neighborhood
            {
                Id = Guid.Parse("adda523f-05e2-47d3-bf9f-b4df36b15b95"),
                Name = "Gaziler Mahallesi",
                DistrictId = Guid.Parse("5455ae93-316f-4f09-b673-41b694961d30"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("21dba5fc-2112-4650-89b7-f07861974d5e"),
                Name = "Fatih Mahallesi",
                DistrictId = Guid.Parse("5455ae93-316f-4f09-b673-41b694961d30"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // İlkadım (DistrictId: 6ed5a42a-2b61-49e0-8dae-68b108c158f6)
            new Neighborhood
            {
                Id = Guid.Parse("22bd9ba9-8a01-40e3-a284-c50cedac828a"),
                Name = "Bahçelievler Mahallesi",
                DistrictId = Guid.Parse("6ed5a42a-2b61-49e0-8dae-68b108c158f6"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("d73a4eb7-2fc4-4099-86ae-f61678333371"),
                Name = "Kurtuluş Mahallesi",
                DistrictId = Guid.Parse("6ed5a42a-2b61-49e0-8dae-68b108c158f6"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Atakum (DistrictId: 89b9eac8-1a1c-415e-8662-55ea01c84871)
            new Neighborhood
            {
                Id = Guid.Parse("947a38fc-04df-4bea-8a19-358669b3feb1"),
                Name = "Deniz Mahallesi",
                DistrictId = Guid.Parse("89b9eac8-1a1c-415e-8662-55ea01c84871"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("632d3752-8628-4b1c-8b6c-0dbeb45157f6"),
                Name = "Yeni Mahallesi",
                DistrictId = Guid.Parse("89b9eac8-1a1c-415e-8662-55ea01c84871"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Ortahisar (DistrictId: 66373676-5a8a-4e3b-9eda-5ba02fdf3cf5)
            new Neighborhood
            {
                Id = Guid.Parse("d2d335fc-9fcd-48ad-a4a2-67c58941df4b"),
                Name = "Yenicuma Mahallesi",
                DistrictId = Guid.Parse("66373676-5a8a-4e3b-9eda-5ba02fdf3cf5"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("555c9b1e-9135-48ce-a085-521ee20c9f01"),
                Name = "Gülbahar Mahallesi",
                DistrictId = Guid.Parse("66373676-5a8a-4e3b-9eda-5ba02fdf3cf5"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Akçaabat (DistrictId: dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f)
            new Neighborhood
            {
                Id = Guid.Parse("df539924-7a12-4dc3-a799-745ef57fa53d"),
                Name = "Dere Mahallesi",
                DistrictId = Guid.Parse("dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("0794b782-e1c3-46e1-afe9-a0ede79048a6"),
                Name = "Yeni Mahallesi",
                DistrictId = Guid.Parse("dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Tepebaşı (DistrictId: 00e75515-6436-48bd-8062-08d0c24e6ac7)
            new Neighborhood
            {
                Id = Guid.Parse("a9dc77d4-9c9d-41cd-b20e-77e338556668"),
                Name = "Porsuk Mahallesi",
                DistrictId = Guid.Parse("00e75515-6436-48bd-8062-08d0c24e6ac7"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("e385fe3a-6499-48f1-9952-bf07e9d6acbe"),
                Name = "Dumlupınar Mahallesi",
                DistrictId = Guid.Parse("00e75515-6436-48bd-8062-08d0c24e6ac7"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },

            // Odunpazarı (DistrictId: 6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb)
            new Neighborhood
            {
                Id = Guid.Parse("73b46a77-97d4-4222-881f-f72338dfe86d"),
                Name = "Vadi Mahallesi",
                DistrictId = Guid.Parse("6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new Neighborhood
            {
                Id = Guid.Parse("ac4a73c3-8362-4846-a8b1-6146d019a3ba"),
                Name = "Kültür Mahallesi",
                DistrictId = Guid.Parse("6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb"),
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            }

        );

        // Ürün oluştur
        var productList = SeedProduct();
        modelBuilder.Entity<Product>().HasData(productList);

        // Müşteri Oluştur
        var customerList = SeedCustomer();
        modelBuilder.Entity<Customer>().HasData(customerList);

        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                Id = Guid.Parse("1d0f4382-6602-4520-896b-c62a0ec0b97c"),
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                ProvinceId = Guid.Parse("e921956a-48ed-47ec-99b5-b6ebe3b3b348"),
                DistrictId = Guid.Parse("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"),
                NeighborhoodId = Guid.Parse("74662223-7112-4e09-a7b9-a4fea757d133"),
                CustomerId = Guid.Parse("a37038d7-d16f-4209-8846-000000000001"),
                AddressLine = "No: 5 Daire : 22 Seyhan / Adana",
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false,
                PostalCode = "1010",
                AddressLine2 = ""
            },
            new Address
            {
                Id = Guid.Parse("bd849fc1-0c6f-4778-9ba8-b352c79df304"),
                CountryId = Guid.Parse("6588ba56-d758-47e7-8c8f-f90a2f46d70a"),
                ProvinceId = Guid.Parse("0df01398-e42b-4f53-b00f-0e5a7a8978b2"),
                DistrictId = Guid.Parse("9f147822-ff2f-4804-ae62-d58931f56d24"),
                NeighborhoodId = Guid.Parse("0b4cb6df-6bd8-4224-9608-3b99e7afe550"),
                CustomerId = Guid.Parse("a37038d7-d16f-4209-8846-000000000002"),
                AddressLine = "No: 7 Daire : 13 Çankaya / Ankara",
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false,
                PostalCode = "06690",
                AddressLine2 = ""
            }            
        );

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
                Price = price,
                Description = description,
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            });
        }

        return products;
    }

    private List<Customer> SeedCustomer()
    {
        var random = new Random(12345);
        List<Customer> customers = new List<Customer>();
        var firstNames = new List<string> { "Ahmet", "Mehmet", "Ayşe", "Fatma", "Emre" };
        var lastNames = new List<string> { "Yılmaz", "Demir", "Çelik", "Kara", "Şahin" };

        for (int i = 0; i < firstNames.Count; i++)
        {
            var name = firstNames[i];
            var surname = lastNames[i];

            var email = $"{name.ToLower()}.{surname.ToLower()}@example.com";
            var phone = $"+90 5{random.Next(300000000, 549999999)}";

            customers.Add(new Customer
            {
                Id = Guid.Parse($"a37038d7-d16f-4209-8846-0000000000{i + 1:D2}"),
                Name = name,
                Surname = surname,
                Email = email,
                PhoneNumber = phone,
                CreatedAt = new DateTime(2025, 6, 26, 1, 15, 0, DateTimeKind.Utc),
                IsDeleted = false
            });
        }

        return customers;
    }

}






