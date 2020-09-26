using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi1.Migrations
{
    public partial class ManualRelationMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Users_ReceiverId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Users_SenderId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_Users_FriendOneId",
                table: "FriendShip");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_Users_FriendTwoId",
                table: "FriendShip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendShip",
                table: "FriendShip");

            migrationBuilder.DropIndex(
                name: "IX_FriendShip_FriendTwoId",
                table: "FriendShip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequest_ReceiverId",
                table: "FriendRequest");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequest_SenderId",
                table: "FriendRequest");

            migrationBuilder.DropColumn(
                name: "FriendOneId",
                table: "FriendShip");

            migrationBuilder.DropColumn(
                name: "FriendTwoId",
                table: "FriendShip");

            migrationBuilder.DropColumn(
                name: "FriendsSince",
                table: "FriendShip");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "FriendRequest");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "FriendRequest");

            migrationBuilder.RenameTable(
                name: "FriendShip",
                newName: "Friends");

            migrationBuilder.RenameTable(
                name: "FriendRequest",
                newName: "FriendRequests");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Friends",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Friends",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FriendRequests",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Users_UserId",
                table: "FriendRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_UserId",
                table: "Friends",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Users_UserId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_UserId",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_UserId",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_UserId",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FriendRequests");

            migrationBuilder.RenameTable(
                name: "Friends",
                newName: "FriendShip");

            migrationBuilder.RenameTable(
                name: "FriendRequests",
                newName: "FriendRequest");

            migrationBuilder.AddColumn<string>(
                name: "FriendOneId",
                table: "FriendShip",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FriendTwoId",
                table: "FriendShip",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FriendsSince",
                table: "FriendShip",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "FriendRequest",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "FriendRequest",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendShip",
                table: "FriendShip",
                columns: new[] { "FriendOneId", "FriendTwoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FriendShip_FriendTwoId",
                table: "FriendShip",
                column: "FriendTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_ReceiverId",
                table: "FriendRequest",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequest_SenderId",
                table: "FriendRequest",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Users_ReceiverId",
                table: "FriendRequest",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Users_SenderId",
                table: "FriendRequest",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_Users_FriendOneId",
                table: "FriendShip",
                column: "FriendOneId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_Users_FriendTwoId",
                table: "FriendShip",
                column: "FriendTwoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
