using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityExplorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Taghelpers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Reviews");
        }
    }
}
