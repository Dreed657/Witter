using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Witter.Data.Migrations
{
    public partial class FollowMappingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_FollowerId",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_FollowingId",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "FollowerId",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "FollowingId",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserFollowers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserFollowers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserFollowers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RevicerId",
                table: "UserFollowers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "UserFollowers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_IsDeleted",
                table: "UserFollowers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_RevicerId",
                table: "UserFollowers",
                column: "RevicerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_SenderId",
                table: "UserFollowers",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_AspNetUsers_RevicerId",
                table: "UserFollowers",
                column: "RevicerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowers_AspNetUsers_SenderId",
                table: "UserFollowers",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowers_AspNetUsers_RevicerId",
                table: "UserFollowers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowers_AspNetUsers_SenderId",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_IsDeleted",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_RevicerId",
                table: "UserFollowers");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowers_SenderId",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "RevicerId",
                table: "UserFollowers");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "UserFollowers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserFollowers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FollowerId",
                table: "UserFollowers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FollowingId",
                table: "UserFollowers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserFollowers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowerId",
                table: "UserFollowers",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowers_FollowingId",
                table: "UserFollowers",
                column: "FollowingId");
        }
    }
}
