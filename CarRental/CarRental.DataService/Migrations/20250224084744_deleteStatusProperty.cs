using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.DataService.Migrations
{
    /// <inheritdoc />
    public partial class deleteStatusProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VehicleReturns");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RentalTransactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RentalBookings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Maintenances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Vehicles",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "VehicleReturns",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "RentalTransactions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "RentalBookings",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Maintenances",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
