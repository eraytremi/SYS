using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, false, "Meyve", null, null },
                    { 2, null, null, null, null, false, "Sebze", null, null },
                    { 3, null, null, null, null, false, "Et Ürünleri", null, null },
                    { 4, null, null, null, null, false, "Süt Ürünleri", null, null },
                    { 5, null, null, null, null, false, "Tatlılar", null, null },
                    { 6, null, null, null, null, false, "Kahvaltılıklar", null, null },
                    { 7, null, null, null, null, false, "Deniz Ürünleri", null, null },
                    { 8, null, null, null, null, false, "Kuru Yemişler", null, null },
                    { 9, null, null, null, null, false, "İçecekler", null, null },
                    { 10, null, null, null, null, false, "Baharatlar", null, null },
                    { 11, null, null, null, null, false, "Saklama Kabı", null, null },
                    { 12, null, null, null, null, false, "Kahve", null, null },
                    { 13, null, null, null, null, false, "Çay", null, null },
                    { 14, null, null, null, null, false, "Dondurma", null, null },
                    { 15, null, null, null, null, false, "Kuruyemiş", null, null },
                    { 16, null, null, null, null, false, "Atıştırmalık", null, null },
                    { 17, null, null, null, null, false, "Un ve Unlu Mamüller", null, null },
                    { 18, null, null, null, null, false, "Pasta", null, null },
                    { 19, null, null, null, null, false, "Pilavlık ve Bulgur", null, null },
                    { 20, null, null, null, null, false, "Konserve ve Salça", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
