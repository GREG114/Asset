using Microsoft.EntityFrameworkCore.Migrations;

namespace LxGreg.Migrations
{
    public partial class take : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "take",
                table: "orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "take",
                table: "orders");
        }
    }
}
