using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSKUColumnAndAddedProductSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sku",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "CreatedAt", "DeletedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3027ccfd-d16f-4209-8846-000000000001"), 9925.06m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Laptop", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000002"), 14860.37m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Akıllı Telefon", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000003"), 4061.84m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Tablet", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000004"), 11921.34m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Bluetooth Kulaklık", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000005"), 7505.19m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Akıllı Saat", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000006"), 3827.2m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Harici Disk", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000007"), 4091.75m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Kamera", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000008"), 5943.31m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Oyun Konsolu", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000009"), 8054.15m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "SSD Disk", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000010"), 14925.49m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Kablosuz Mouse", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000011"), 3397.15m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Akıllı Saat", null },
                    { new Guid("3027ccfd-d16f-4209-8846-000000000012"), 6796.93m, new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Laptop Çantası", null }
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

            migrationBuilder.AddColumn<string>(
                name: "Sku",
                table: "Products",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
