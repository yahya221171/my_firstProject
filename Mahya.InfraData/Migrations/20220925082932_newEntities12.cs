using Microsoft.EntityFrameworkCore.Migrations;

namespace Mahya.InfraData.Migrations
{
    public partial class newEntities12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewPrice",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Products");
        }
    }
}
