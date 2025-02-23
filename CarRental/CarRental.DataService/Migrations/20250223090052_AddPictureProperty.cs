using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.DataService.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Vehicles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "CarCategories",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "CarCategories");
        }
    }
}
