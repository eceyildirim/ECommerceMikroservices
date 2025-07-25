using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNeighborhoodSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Neighborhoods",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "DistrictId", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0794b782-e1c3-46e1-afe9-a0ede79048a6"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f"), false, "Yeni Mahallesi", null },
                    { new Guid("0b4cb6df-6bd8-4224-9608-3b99e7afe550"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"), false, "Kızılay Mahallesi", null },
                    { new Guid("0f013f95-53ab-4f4c-b7c7-b79da2ca5cb5"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("0e76fb72-eeb1-454c-867d-d4b616cc20aa"), false, "İstiklal Mahallesi", null },
                    { new Guid("21dba5fc-2112-4650-89b7-f07861974d5e"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("5455ae93-316f-4f09-b673-41b694961d30"), false, "Fatih Mahallesi", null },
                    { new Guid("22bd9ba9-8a01-40e3-a284-c50cedac828a"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("6ed5a42a-2b61-49e0-8dae-68b108c158f6"), false, "Bahçelievler Mahallesi", null },
                    { new Guid("248d4d1b-158b-4bfd-9064-3a0d70490a51"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("0e76fb72-eeb1-454c-867d-d4b616cc20aa"), false, "Fatih Mahallesi", null },
                    { new Guid("29ab92d7-7586-413d-8041-a42b6f6372b5"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("4ca79d56-97f7-41e7-a921-81ae203b6a9f"), false, "Beşevler Mahallesi", null },
                    { new Guid("2e723ede-eba0-48dc-bacc-397ce65d95dd"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("9f147822-ff2f-4804-ae62-d58931f56d24"), false, "Toros Mahallesi", null },
                    { new Guid("4646c942-1d48-42cd-bf67-8827ae9edcb5"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("ef45957b-2e92-40a0-b8d8-5591c3538b56"), false, "Çay Mahallesi", null },
                    { new Guid("4f05ea49-74b4-488c-87bc-4aa286e82b78"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("ab0e7e28-a602-43dd-b186-97551527a4cf"), false, "Hurma Mahallesi", null },
                    { new Guid("514ca6da-e656-4814-af7d-29a7220e5e85"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("ab0e7e28-a602-43dd-b186-97551527a4cf"), false, "Gürsu Mahallesi", null },
                    { new Guid("52ca3967-9b24-4878-82b0-242cee442647"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("15b653d0-d69d-4e82-abbb-fc605b20bd88"), false, "Bosna Hersek Mahallesi", null },
                    { new Guid("555c9b1e-9135-48ce-a085-521ee20c9f01"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("66373676-5a8a-4e3b-9eda-5ba02fdf3cf5"), false, "Gülbahar Mahallesi", null },
                    { new Guid("5ba61236-c1c0-4517-b2f8-a8a0fe679cda"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("15b653d0-d69d-4e82-abbb-fc605b20bd88"), false, "Fevzi Çakmak Mahallesi", null },
                    { new Guid("632d3752-8628-4b1c-8b6c-0dbeb45157f6"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("89b9eac8-1a1c-415e-8662-55ea01c84871"), false, "Yeni Mahallesi", null },
                    { new Guid("6477848e-7d2b-446d-adff-4c742b57be13"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("4ca79d56-97f7-41e7-a921-81ae203b6a9f"), false, "Yıldırım Mahallesi", null },
                    { new Guid("65942c42-a6b5-4af0-b63d-0a6e3d3df99e"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("852d995d-6bdf-4336-b9ef-2ba939f51107"), false, "Ataevler Mahallesi", null },
                    { new Guid("6a996b27-4df6-459a-8fbc-8f3216ea11bf"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("e89d05a3-77c3-4083-91f9-48a69220ef08"), false, "Altınkum Mahallesi", null },
                    { new Guid("73b46a77-97d4-4222-881f-f72338dfe86d"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb"), false, "Vadi Mahallesi", null },
                    { new Guid("74662223-7112-4e09-a7b9-a4fea757d133"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("9f147822-ff2f-4804-ae62-d58931f56d24"), false, "Bahçe Mahallesi", null },
                    { new Guid("947a38fc-04df-4bea-8a19-358669b3feb1"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("89b9eac8-1a1c-415e-8662-55ea01c84871"), false, "Deniz Mahallesi", null },
                    { new Guid("a88b21b4-2df1-40c2-bc92-1d216644b789"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("ef45957b-2e92-40a0-b8d8-5591c3538b56"), false, "Fettah Mahallesi", null },
                    { new Guid("a9dc77d4-9c9d-41cd-b20e-77e338556668"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("00e75515-6436-48bd-8062-08d0c24e6ac7"), false, "Porsuk Mahallesi", null },
                    { new Guid("aa3a6fa6-7e6a-4a56-ae0e-14bd16e696fb"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("a4da7b97-d21b-45ae-b5d9-eb8217ba4101"), false, "Bahçelievler Mahallesi", null },
                    { new Guid("ab1872a3-a512-472d-98ea-81febc6bc7ba"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("852d995d-6bdf-4336-b9ef-2ba939f51107"), false, "Nilüfer Mahallesi", null },
                    { new Guid("ac4a73c3-8362-4846-a8b1-6146d019a3ba"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("6ed7f6fa-ef5f-4f6a-bad1-ec2c6c9696bb"), false, "Kültür Mahallesi", null },
                    { new Guid("adda523f-05e2-47d3-bf9f-b4df36b15b95"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("5455ae93-316f-4f09-b673-41b694961d30"), false, "Gaziler Mahallesi", null },
                    { new Guid("cd60fc90-b557-4682-b394-ca8461182da3"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("a4da7b97-d21b-45ae-b5d9-eb8217ba4101"), false, "Şehitler Mahallesi", null },
                    { new Guid("d00cdc4c-6924-4ea9-a220-d0a5953df310"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("fa8a453d-0342-4a59-a356-1b7e1a335b52"), false, "Etlik Mahallesi", null },
                    { new Guid("d2d335fc-9fcd-48ad-a4a2-67c58941df4b"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("66373676-5a8a-4e3b-9eda-5ba02fdf3cf5"), false, "Yenicuma Mahallesi", null },
                    { new Guid("d4a93ddc-5472-46a3-8327-d217948504dd"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("e89d05a3-77c3-4083-91f9-48a69220ef08"), false, "Çağlayan Mahallesi", null },
                    { new Guid("d73a4eb7-2fc4-4099-86ae-f61678333371"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("6ed5a42a-2b61-49e0-8dae-68b108c158f6"), false, "Kurtuluş Mahallesi", null },
                    { new Guid("df539924-7a12-4dc3-a799-745ef57fa53d"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("dc7d5f8f-84b3-4e0b-ad0a-a50b6179d51f"), false, "Dere Mahallesi", null },
                    { new Guid("e385fe3a-6499-48f1-9952-bf07e9d6acbe"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("00e75515-6436-48bd-8062-08d0c24e6ac7"), false, "Dumlupınar Mahallesi", null },
                    { new Guid("f376d0eb-789c-43e4-862c-d4fbd2ad71e2"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("ef8627cc-ad2b-4fa7-9450-4f570dd1f63e"), false, "Bahçelievler Mahallesi", null },
                    { new Guid("fbcb61da-c522-4820-a722-7a5eff1aeb9d"), new DateTime(2025, 6, 26, 1, 15, 0, 0, DateTimeKind.Utc), null, new Guid("fa8a453d-0342-4a59-a356-1b7e1a335b52"), false, "Aşağı Eğlence Mahallesi", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("0794b782-e1c3-46e1-afe9-a0ede79048a6"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("0b4cb6df-6bd8-4224-9608-3b99e7afe550"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("0f013f95-53ab-4f4c-b7c7-b79da2ca5cb5"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("21dba5fc-2112-4650-89b7-f07861974d5e"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("22bd9ba9-8a01-40e3-a284-c50cedac828a"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("248d4d1b-158b-4bfd-9064-3a0d70490a51"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("29ab92d7-7586-413d-8041-a42b6f6372b5"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("2e723ede-eba0-48dc-bacc-397ce65d95dd"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("4646c942-1d48-42cd-bf67-8827ae9edcb5"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("4f05ea49-74b4-488c-87bc-4aa286e82b78"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("514ca6da-e656-4814-af7d-29a7220e5e85"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("52ca3967-9b24-4878-82b0-242cee442647"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("555c9b1e-9135-48ce-a085-521ee20c9f01"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("5ba61236-c1c0-4517-b2f8-a8a0fe679cda"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("632d3752-8628-4b1c-8b6c-0dbeb45157f6"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("6477848e-7d2b-446d-adff-4c742b57be13"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("65942c42-a6b5-4af0-b63d-0a6e3d3df99e"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("6a996b27-4df6-459a-8fbc-8f3216ea11bf"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("73b46a77-97d4-4222-881f-f72338dfe86d"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("74662223-7112-4e09-a7b9-a4fea757d133"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("947a38fc-04df-4bea-8a19-358669b3feb1"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("a88b21b4-2df1-40c2-bc92-1d216644b789"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("a9dc77d4-9c9d-41cd-b20e-77e338556668"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("aa3a6fa6-7e6a-4a56-ae0e-14bd16e696fb"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("ab1872a3-a512-472d-98ea-81febc6bc7ba"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("ac4a73c3-8362-4846-a8b1-6146d019a3ba"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("adda523f-05e2-47d3-bf9f-b4df36b15b95"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("cd60fc90-b557-4682-b394-ca8461182da3"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("d00cdc4c-6924-4ea9-a220-d0a5953df310"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("d2d335fc-9fcd-48ad-a4a2-67c58941df4b"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("d4a93ddc-5472-46a3-8327-d217948504dd"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("d73a4eb7-2fc4-4099-86ae-f61678333371"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("df539924-7a12-4dc3-a799-745ef57fa53d"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("e385fe3a-6499-48f1-9952-bf07e9d6acbe"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("f376d0eb-789c-43e4-862c-d4fbd2ad71e2"));

            migrationBuilder.DeleteData(
                table: "Neighborhoods",
                keyColumn: "Id",
                keyValue: new Guid("fbcb61da-c522-4820-a722-7a5eff1aeb9d"));
        }
    }
}
