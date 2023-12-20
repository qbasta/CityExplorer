using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityExplorer.Migrations
{
    /// <inheritdoc />
    public partial class PrivateLandmarkList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLandmarkLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSaved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLandmarkLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLandmarkLists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLandmarks",
                columns: table => new
                {
                    LandmarkId = table.Column<int>(type: "int", nullable: false),
                    UserLandmarkListId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLandmarks", x => new { x.LandmarkId, x.UserLandmarkListId });
                    table.ForeignKey(
                        name: "FK_UserLandmarks_Landmarks_LandmarkId",
                        column: x => x.LandmarkId,
                        principalTable: "Landmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLandmarks_UserLandmarkLists_UserLandmarkListId",
                        column: x => x.UserLandmarkListId,
                        principalTable: "UserLandmarkLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLandmarkLists_AppUserId",
                table: "UserLandmarkLists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLandmarks_UserLandmarkListId",
                table: "UserLandmarks",
                column: "UserLandmarkListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLandmarks");

            migrationBuilder.DropTable(
                name: "UserLandmarkLists");
        }
    }
}
