namespace Witter.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddImagesOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Media",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_CreatorId",
                table: "Media",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_AspNetUsers_CreatorId",
                table: "Media",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_AspNetUsers_CreatorId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_CreatorId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Media");
        }
    }
}
