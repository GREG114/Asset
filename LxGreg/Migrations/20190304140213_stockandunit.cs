using Microsoft.EntityFrameworkCore.Migrations;

namespace LxGreg.Migrations
{
    public partial class stockandunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_units_UnitId",
                table: "stocks");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "stocks",
                newName: "unitId");

            migrationBuilder.RenameIndex(
                name: "IX_stocks_UnitId",
                table: "stocks",
                newName: "IX_stocks_unitId");

            migrationBuilder.AlterColumn<int>(
                name: "unitId",
                table: "stocks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_units_unitId",
                table: "stocks",
                column: "unitId",
                principalTable: "units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_units_unitId",
                table: "stocks");

            migrationBuilder.RenameColumn(
                name: "unitId",
                table: "stocks",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_stocks_unitId",
                table: "stocks",
                newName: "IX_stocks_UnitId");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "stocks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_units_UnitId",
                table: "stocks",
                column: "UnitId",
                principalTable: "units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
