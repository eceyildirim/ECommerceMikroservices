using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "IsDeleted", "Name", "PhoneNumber", "Surname", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a37038d7-d16f-4209-8846-000000000001"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "ahmet.yılmaz@example.com", false, "Ahmet", "+90 5316686733", "Yılmaz", null },
                    { new Guid("a37038d7-d16f-4209-8846-000000000002"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "mehmet.demir@example.com", false, "Mehmet", "+90 5317539877", "Demir", null },
                    { new Guid("a37038d7-d16f-4209-8846-000000000003"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "ayşe.çelik@example.com", false, "Ayşe", "+90 5493691283", "Çelik", null },
                    { new Guid("a37038d7-d16f-4209-8846-000000000004"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "fatma.kara@example.com", false, "Fatma", "+90 5427784816", "Kara", null },
                    { new Guid("a37038d7-d16f-4209-8846-000000000005"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, "emre.şahin@example.com", false, "Emre", "+90 5499372638", "Şahin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a37038d7-d16f-4209-8846-000000000001"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a37038d7-d16f-4209-8846-000000000002"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a37038d7-d16f-4209-8846-000000000003"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a37038d7-d16f-4209-8846-000000000004"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a37038d7-d16f-4209-8846-000000000005"));
        }
    }
}
