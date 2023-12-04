using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityExplorer.Data.Migrations
{
    /// <inheritdoc />
    public partial class userlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoutes_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLandmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LandmarkId = table.Column<int>(type: "int", nullable: false),
                    UserRouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLandmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLandmarks_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLandmarks_Landmarks_LandmarkId",
                        column: x => x.LandmarkId,
                        principalTable: "Landmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLandmarks_UserRoutes_UserRouteId",
                        column: x => x.UserRouteId,
                        principalTable: "UserRoutes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLandmarks_AppUserId",
                table: "UserLandmarks",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLandmarks_LandmarkId",
                table: "UserLandmarks",
                column: "LandmarkId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLandmarks_UserRouteId",
                table: "UserLandmarks",
                column: "UserRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoutes_AppUserId",
                table: "UserRoutes",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLandmarks");

            migrationBuilder.DropTable(
                name: "UserRoutes");
        }
    }
}
