using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Witter.Data.Migrations
{
    public partial class AddBaseDeletebleModelToFollowers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserFollowers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserFollowers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserFollowers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserFollowers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserFollowers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_IsDeleted",
                table: "UserFollowers",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_IsDeleted",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserFollowers");
        }
    }
}
