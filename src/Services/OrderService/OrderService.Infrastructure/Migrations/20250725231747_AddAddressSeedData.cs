using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressLine", "AddressLine2", "CountryId", "CreatedAt", "CustomerId", "DeletedAt", "DistrictId", "IsDeleted", "NeighborhoodId", "PostalCode", "ProvinceId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1d0f4382-6602-4520-896b-c62a0ec0b97c"), "No: 5 Daire : 22 Seyhan / Adana", "", new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), new Guid("a37038d7-d16f-4209-8846-000000000001"), null, new Guid("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"), false, new Guid("74662223-7112-4e09-a7b9-a4fea757d133"), "1010", new Guid("e921956a-48ed-47ec-99b5-b6ebe3b3b348"), null },
                    { new Guid("bd849fc1-0c6f-4778-9ba8-b352c79df304"), "No: 7 Daire : 13 Çankaya / Ankara", "", new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), new Guid("a37038d7-d16f-4209-8846-000000000002"), null, new Guid("9f147822-ff2f-4804-ae62-d58931f56d24"), false, new Guid("0b4cb6df-6bd8-4224-9608-3b99e7afe550"), "06690", new Guid("0df01398-e42b-4f53-b00f-0e5a7a8978b2"), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("1d0f4382-6602-4520-896b-c62a0ec0b97c"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("bd849fc1-0c6f-4778-9ba8-b352c79df304"));
        }
    }
}
