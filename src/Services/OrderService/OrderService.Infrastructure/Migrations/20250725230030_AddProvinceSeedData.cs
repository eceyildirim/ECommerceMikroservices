using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProvinceSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "CreatedAt", "DeletedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0bf70dca-87a1-4e40-a0c1-5b2dc6767afb"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Antalya", null },
                    { new Guid("0df01398-e42b-4f53-b00f-0e5a7a8978b2"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Adana", null },
                    { new Guid("16599411-030b-495e-8a6c-6a3af09b1efc"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "İzmir", null },
                    { new Guid("52415f74-3537-4ed9-a17b-ed31d0e18aea"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Trabzon", null },
                    { new Guid("56388bfa-007b-4742-a9c1-0bfc07766fa8"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Gaziantep", null },
                    { new Guid("7715dff3-417b-46d1-8085-c9e857a9589b"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Eskişehir", null },
                    { new Guid("922e3d42-5970-48db-b164-bbba26ea1816"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Bursa", null },
                    { new Guid("bcf25752-d7f4-4c90-a597-18b9371d4ddb"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Samsun", null },
                    { new Guid("cdc09180-6dd8-4b24-8ddd-b7b2ef463310"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Konya", null },
                    { new Guid("e921956a-48ed-47ec-99b5-b6ebe3b3b348"), new Guid("6588ba56-d758-47e7-8c8f-f90a2f46d70a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Ankara", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("0bf70dca-87a1-4e40-a0c1-5b2dc6767afb"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("0df01398-e42b-4f53-b00f-0e5a7a8978b2"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("16599411-030b-495e-8a6c-6a3af09b1efc"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("52415f74-3537-4ed9-a17b-ed31d0e18aea"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("56388bfa-007b-4742-a9c1-0bfc07766fa8"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("7715dff3-417b-46d1-8085-c9e857a9589b"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("922e3d42-5970-48db-b164-bbba26ea1816"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("bcf25752-d7f4-4c90-a597-18b9371d4ddb"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("cdc09180-6dd8-4b24-8ddd-b7b2ef463310"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("e921956a-48ed-47ec-99b5-b6ebe3b3b348"));
        }
    }
}
