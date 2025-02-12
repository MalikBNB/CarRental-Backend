using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.DataService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CarCategories");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Vehicles",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Vehicles",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "VehicleReturns",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "VehicleReturns",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "RentalTransactions",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "RentalTransactions",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "RentalBookings",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "RentalBookings",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "Maintenances",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Maintenances",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "ModifierId",
                table: "CarCategories",
                newName: "Modifier");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "CarCategories",
                newName: "Creator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "Vehicles",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Vehicles",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "VehicleReturns",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "VehicleReturns",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "RentalTransactions",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "RentalTransactions",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "RentalBookings",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "RentalBookings",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "Maintenances",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Maintenances",
                newName: "CreatorId");

            migrationBuilder.RenameColumn(
                name: "Modifier",
                table: "CarCategories",
                newName: "ModifierId");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "CarCategories",
                newName: "CreatorId");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "CarCategories",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
