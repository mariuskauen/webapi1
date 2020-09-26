using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi1.Migrations
{
    public partial class ChangedFriendShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FriendsFrom",
                table: "Friends",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendsFrom",
                table: "Friends");
        }
    }
}
