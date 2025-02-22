using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TweetCopycat.Migrations
{
    /// <inheritdoc />
    public partial class ControllerUserModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserModelId",
                table: "Follows",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserModelId",
                table: "Follows",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_AspNetUsers_UserModelId",
                table: "Follows",
                column: "UserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_AspNetUsers_UserModelId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_UserModelId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Follows");
        }
    }
}
