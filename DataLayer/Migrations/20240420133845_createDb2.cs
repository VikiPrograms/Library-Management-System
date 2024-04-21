using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class createDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ReadingCards_ReadingCardId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "ReadingCardId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ReadingCards_ReadingCardId",
                table: "Books",
                column: "ReadingCardId",
                principalTable: "ReadingCards",
                principalColumn: "ReadingCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ReadingCards_ReadingCardId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "ReadingCardId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ReadingCards_ReadingCardId",
                table: "Books",
                column: "ReadingCardId",
                principalTable: "ReadingCards",
                principalColumn: "ReadingCardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
