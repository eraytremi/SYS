using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig78 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "StockStatuses");

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                table: "StockMovements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusType",
                table: "StockMovements");

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                table: "StockStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
