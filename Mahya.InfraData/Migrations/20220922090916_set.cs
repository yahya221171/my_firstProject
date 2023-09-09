using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahya.InfraData.Migrations
{
    public partial class set : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWallets_Users_UserId",
                table: "UserWallets");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWallets_Users_UserId",
                table: "UserWallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWallets_Users_UserId",
                table: "UserWallets");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWallets_Users_UserId",
                table: "UserWallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
