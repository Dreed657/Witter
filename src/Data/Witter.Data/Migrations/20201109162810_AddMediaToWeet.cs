using Microsoft.EntityFrameworkCore.Migrations;

namespace Witter.Data.Migrations
{
    public partial class AddMediaToWeet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Weets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weets_ImageId",
                table: "Weets",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weets_Media_ImageId",
                table: "Weets",
                column: "ImageId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weets_Media_ImageId",
                table: "Weets");

            migrationBuilder.DropIndex(
                name: "IX_Weets_ImageId",
                table: "Weets");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Weets");
        }
    }
}
