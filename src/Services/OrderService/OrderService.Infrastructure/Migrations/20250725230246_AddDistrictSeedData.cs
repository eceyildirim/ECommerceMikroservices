using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDistrictSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "Name", "ProvinceId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00e75515-6436-48bd-8062-08d0c24e6ac7"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Tepebaşı", new Guid("7715dff3-417b-46d1-8085-c9e857a9589b"), null },
                    { new Guid("0e76fb72-eeb1-454c-867d-d4b616cc20aa"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Meram", new Guid("cdc09180-6dd8-4b24-8ddd-b7b2ef463310"), null },
                    { new Guid("15b653d0-d69d-4e82-abbb-fc605b20bd88"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Selçuklu", new Guid("cdc09180-6dd8-4b24-8ddd-b7b2ef463310"), null },
                    { new Guid("432eac18-6f63-44e2-9f0a-7a9790a3cb7c"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Bornova", new Guid("16599411-030b-495e-8a6c-6a3af09b1efc"), null },
                    { new Guid("4ca79d56-97f7-41e7-a921-81ae203b6a9f"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Osmangazi", new Guid("922e3d42-5970-48db-b164-bbba26ea1816"), null },
                    { new Guid("5455ae93-316f-4f09-b673-41b694961d30"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Şehitkamil", new Guid("56388bfa-007b-4742-a9c1-0bfc07766fa8"), null },
                    { new Guid("5fad29e4-730b-43d2-8dcb-25800353d3b8"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Karşıyaka", new Guid("16599411-030b-495e-8a6c-6a3af09b1efc"), null },
                    { new Guid("66373676-5a8a-4e3b-9eda-5ba02fdf3cf5"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Ortahisar", new Guid("52415f74-3537-4ed9-a17b-ed31d0e18aea"), null },
                    { new Guid("6ed5a42a-2b61-49e0-8dae-68b108c158f6"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "İlkadım", new Guid("bcf25752-d7f4-4c90-a597-18b9371d4ddb"), null },
                    { new Guid("6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Odunpazarı", new Guid("7715dff3-417b-46d1-8085-c9e857a9589b"), null },
                    { new Guid("852d995d-6bdf-4336-b9ef-2ba939f51107"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Nilüfer", new Guid("922e3d42-5970-48db-b164-bbba26ea1816"), null },
                    { new Guid("89b9eac8-1a1c-415e-8662-55ea01c84871"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Atakum", new Guid("bcf25752-d7f4-4c90-a597-18b9371d4ddb"), null },
                    { new Guid("9f147822-ff2f-4804-ae62-d58931f56d24"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Seyhan", new Guid("0df01398-e42b-4f53-b00f-0e5a7a8978b2"), null },
                    { new Guid("a4da7b97-d21b-45ae-b5d9-eb8217ba4101"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Şahinbey", new Guid("56388bfa-007b-4742-a9c1-0bfc07766fa8"), null },
                    { new Guid("ab0e7e28-a602-43dd-b186-97551527a4cf"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Konyaaltı", new Guid("0bf70dca-87a1-4e40-a0c1-5b2dc6767afb"), null },
                    { new Guid("dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Akçaabat", new Guid("52415f74-3537-4ed9-a17b-ed31d0e18aea"), null },
                    { new Guid("e89d05a3-77c3-4083-91f9-48a69220ef08"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Muratpaşa", new Guid("0bf70dca-87a1-4e40-a0c1-5b2dc6767afb"), null },
                    { new Guid("ef45957b-2e92-40a0-b8d8-5591c3538b56"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Çukurova", new Guid("0df01398-e42b-4f53-b00f-0e5a7a8978b2"), null },
                    { new Guid("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Çankaya", new Guid("e921956a-48ed-47ec-99b5-b6ebe3b3b348"), null },
                    { new Guid("fa8a453d-0342-4a59-a356-1b7e1a335b52"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, false, "Keçiören", new Guid("e921956a-48ed-47ec-99b5-b6ebe3b3b348"), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("00e75515-6436-48bd-8062-08d0c24e6ac7"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("0e76fb72-eeb1-454c-867d-d4b616cc20aa"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("15b653d0-d69d-4e82-abbb-fc605b20bd88"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("432eac18-6f63-44e2-9f0a-7a9790a3cb7c"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("4ca79d56-97f7-41e7-a921-81ae203b6a9f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("5455ae93-316f-4f09-b673-41b694961d30"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("5fad29e4-730b-43d2-8dcb-25800353d3b8"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("66373676-5a8a-4e3b-9eda-5ba02fdf3cf5"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("6ed5a42a-2b61-49e0-8dae-68b108c158f6"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("852d995d-6bdf-4336-b9ef-2ba939f51107"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("89b9eac8-1a1c-415e-8662-55ea01c84871"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("9f147822-ff2f-4804-ae62-d58931f56d24"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("a4da7b97-d21b-45ae-b5d9-eb8217ba4101"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("ab0e7e28-a602-43dd-b186-97551527a4cf"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("e89d05a3-77c3-4083-91f9-48a69220ef08"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("ef45957b-2e92-40a0-b8d8-5591c3538b56"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"));

            migrationBuilder.DeleteData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: new Guid("fa8a453d-0342-4a59-a356-1b7e1a335b52"));
        }
    }
}
