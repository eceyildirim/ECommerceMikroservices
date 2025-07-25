using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3027ccfd-d16f-4209-8846-000000000001"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Laptop Fiyatı: 9925.06 TL.", false, "Laptop", 9925.06m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000002"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Akıllı Telefon Fiyatı: 14860.37 TL.", false, "Akıllı Telefon", 14860.37m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000003"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Tablet Fiyatı: 4061.84 TL.", false, "Tablet", 4061.84m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000004"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Bluetooth Kulaklık Fiyatı: 11921.34 TL.", false, "Bluetooth Kulaklık", 11921.34m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000005"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Akıllı Saat Fiyatı: 7505.19 TL.", false, "Akıllı Saat", 7505.19m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000006"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Harici Disk Fiyatı: 3827.2 TL.", false, "Harici Disk", 3827.2m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000007"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Kamera Fiyatı: 4091.75 TL.", false, "Kamera", 4091.75m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000008"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Oyun Konsolu Fiyatı: 5943.31 TL.", false, "Oyun Konsolu", 5943.31m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000009"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: SSD Disk Fiyatı: 8054.15 TL.", false, "SSD Disk", 8054.15m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000010"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Kablosuz Mouse Fiyatı: 14925.49 TL.", false, "Kablosuz Mouse", 14925.49m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000011"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Akıllı Saat Fiyatı: 3397.15 TL.", false, "Akıllı Saat", 3397.15m, null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000012"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "Ürün Bilgisi: Laptop Çantası Fiyatı: 6796.93 TL.", false, "Laptop Çantası", 6796.93m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000005"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000007"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000008"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000009"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000010"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000011"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3027ccfd-d16f-4209-8846-000000000012"));
        }
    }
}
