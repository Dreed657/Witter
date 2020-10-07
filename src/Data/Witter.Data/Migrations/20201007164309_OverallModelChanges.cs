using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Witter.Data.Migrations
{
    public partial class OverallModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_IsDeleted",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Weets");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserFollowers");

            migrationBuilder.CreateTable(
                name: "WeetLikes",
                columns: table => new
                {
                    ParentId = table.Column<string>(nullable: false),
                    WeetId = table.Column<string>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsLiked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeetLikes", x => new { x.ParentId, x.WeetId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeetLikes");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Weets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserFollowers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserFollowers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_IsDeleted",
                table: "UserFollowers",
                column: "IsDeleted");
        }
    }
}
