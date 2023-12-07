using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityExplorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Reviews");
        }
    }
}
