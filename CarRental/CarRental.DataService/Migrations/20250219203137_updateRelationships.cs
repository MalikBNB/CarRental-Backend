using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.DataService.Migrations
{
    /// <inheritdoc />
    public partial class updateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Vehicles_VehicleId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalBookings_RentalTransactions_RentalTransactionId",
                table: "RentalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalBookings_Users_CustomerId",
                table: "RentalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalBookings_Vehicles_VehicleId",
                table: "RentalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCategories_CarCategoryId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_RentalBookings_RentalTransactionId",
                table: "RentalBookings");

            migrationBuilder.CreateIndex(
                name: "IX_RentalTransactions_RentalBookingId",
                table: "RentalTransactions",
                column: "RentalBookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Vehicles_VehicleId",
                table: "Maintenances",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalBookings_Users_CustomerId",
                table: "RentalBookings",
                column: "CustomerId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalBookings_Vehicles_VehicleId",
                table: "RentalBookings",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalTransactions_RentalBookings_RentalBookingId",
                table: "RentalTransactions",
                column: "RentalBookingId",
                principalTable: "RentalBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCategories_CarCategoryId",
                table: "Vehicles",
                column: "CarCategoryId",
                principalTable: "CarCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Vehicles_VehicleId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalBookings_Users_CustomerId",
                table: "RentalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalBookings_Vehicles_VehicleId",
                table: "RentalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalTransactions_RentalBookings_RentalBookingId",
                table: "RentalTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_CarCategories_CarCategoryId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_RentalTransactions_RentalBookingId",
                table: "RentalTransactions");

            migrationBuilder.CreateIndex(
                name: "IX_RentalBookings_RentalTransactionId",
                table: "RentalBookings",
                column: "RentalTransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Vehicles_VehicleId",
                table: "Maintenances",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalBookings_RentalTransactions_RentalTransactionId",
                table: "RentalBookings",
                column: "RentalTransactionId",
                principalTable: "RentalTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalBookings_Users_CustomerId",
                table: "RentalBookings",
                column: "CustomerId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalBookings_Vehicles_VehicleId",
                table: "RentalBookings",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_CarCategories_CarCategoryId",
                table: "Vehicles",
                column: "CarCategoryId",
                principalTable: "CarCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
