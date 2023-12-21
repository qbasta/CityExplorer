using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityExplorer.Migrations
{
    /// <inheritdoc />
    public partial class LandmarkMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Landmarks");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Landmarks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Landmarks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Landmarks");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
