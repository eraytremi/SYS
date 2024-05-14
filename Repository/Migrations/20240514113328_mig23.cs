using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockStatuses_WareHouses_WareHouseId",
                table: "StockStatuses");

            migrationBuilder.DropIndex(
                name: "IX_StockStatuses_WareHouseId",
                table: "StockStatuses");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "StockStatuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WareHouseId",
                table: "StockStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockStatuses_WareHouseId",
                table: "StockStatuses",
                column: "WareHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockStatuses_WareHouses_WareHouseId",
                table: "StockStatuses",
                column: "WareHouseId",
                principalTable: "WareHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
