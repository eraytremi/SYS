using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockMovements_WareHouses_WareHouseId",
                table: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_WareHouseId",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "StockMovements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WareHouseId",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_WareHouseId",
                table: "StockMovements",
                column: "WareHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockMovements_WareHouses_WareHouseId",
                table: "StockMovements",
                column: "WareHouseId",
                principalTable: "WareHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
