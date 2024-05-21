using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceDestination",
                table: "StockMovements",
                newName: "Source");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "StockMovements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "StockMovements");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "StockMovements",
                newName: "SourceDestination");
        }
    }
}
