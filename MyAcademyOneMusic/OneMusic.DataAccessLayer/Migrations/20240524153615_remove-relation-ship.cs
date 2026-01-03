using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneMusic.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class removerelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongsListenDetails_AspNetUsers_AppUserId",
                table: "SongsListenDetails");

            migrationBuilder.DropIndex(
                name: "IX_SongsListenDetails_AppUserId",
                table: "SongsListenDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SongsListenDetails_AppUserId",
                table: "SongsListenDetails",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongsListenDetails_AspNetUsers_AppUserId",
                table: "SongsListenDetails",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
