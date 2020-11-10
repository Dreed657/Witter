namespace Witter.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CoverImageId",
                table: "AspNetUsers",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Media_CoverImageId",
                table: "AspNetUsers",
                column: "CoverImageId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Media_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Media_CoverImageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Media_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CoverImageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");
        }
    }
}
