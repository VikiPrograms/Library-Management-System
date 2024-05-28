 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class updateLibrary2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadingCards_AspNetUsers_UserId",
                table: "ReadingCards");

            migrationBuilder.DropIndex(
                name: "IX_ReadingCards_UserId",
                table: "ReadingCards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ReadingCards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ReadingCards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingCards_UserId",
                table: "ReadingCards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingCards_AspNetUsers_UserId",
                table: "ReadingCards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
