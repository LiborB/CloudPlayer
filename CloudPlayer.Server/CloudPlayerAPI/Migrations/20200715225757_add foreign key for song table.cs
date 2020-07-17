using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudPlayerAPI.Migrations
{
    public partial class addforeignkeyforsongtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Song",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Song_UserId",
                table: "Song",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_User_UserId",
                table: "Song",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_User_UserId",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Song_UserId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Song");
        }
    }
}
