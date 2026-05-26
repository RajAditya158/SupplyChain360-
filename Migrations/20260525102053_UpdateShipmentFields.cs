using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supplychain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShipmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ETA",
                table: "Shipments",
                newName: "ExpectedDeliveryDate");

            migrationBuilder.AddColumn<long>(
                name: "SupplierId",
                table: "Shipments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "ExpectedDeliveryDate",
                table: "Shipments",
                newName: "ETA");
        }
    }
}
