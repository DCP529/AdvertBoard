using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class EditForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advert_user_user_id",
                table: "advert");

            migrationBuilder.DropIndex(
                name: "IX_advert_user_id",
                table: "advert");

            migrationBuilder.AddColumn<Guid>(
                name: "UserDbId",
                table: "advert",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_advert_UserDbId",
                table: "advert",
                column: "UserDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_advert_user_UserDbId",
                table: "advert",
                column: "UserDbId",
                principalTable: "user",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_advert_user_UserDbId",
                table: "advert");

            migrationBuilder.DropIndex(
                name: "IX_advert_UserDbId",
                table: "advert");

            migrationBuilder.DropColumn(
                name: "UserDbId",
                table: "advert");

            migrationBuilder.CreateIndex(
                name: "IX_advert_user_id",
                table: "advert",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_advert_user_user_id",
                table: "advert",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
