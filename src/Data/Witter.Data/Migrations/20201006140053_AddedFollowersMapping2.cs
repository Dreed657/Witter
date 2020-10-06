using Microsoft.EntityFrameworkCore.Migrations;

namespace Witter.Data.Migrations
{
    public partial class AddedFollowersMapping2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFollowers",
                columns: table => new
                {
                    ParentId = table.Column<string>(nullable: false),
                    FollowerId = table.Column<string>(nullable: false),
                    IsFollowing = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowers", x => new { x.ParentId, x.FollowerId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollowers");
        }
    }
}
