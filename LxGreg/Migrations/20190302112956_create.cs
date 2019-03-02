using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LxGreg.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemNumber = table.Column<string>(nullable: true),
                    ItemShortNumber = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Mark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoreName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    OperaterId = table.Column<string>(nullable: true),
                    storeId = table.Column<int>(nullable: false),
                    unitId = table.Column<int>(nullable: false),
                    assetId = table.Column<int>(nullable: false),
                    Quntity = table.Column<int>(nullable: false),
                    TakerId1 = table.Column<string>(nullable: true),
                    TakerId = table.Column<int>(nullable: false),
                    Mark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_Manager_OperaterId",
                        column: x => x.OperaterId,
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_Manager_TakerId1",
                        column: x => x.TakerId1,
                        principalTable: "Manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_Asset_assetId",
                        column: x => x.assetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_Store_storeId",
                        column: x => x.storeId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_Unit_unitId",
                        column: x => x.unitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    storeId = table.Column<int>(nullable: false),
                    unitId = table.Column<int>(nullable: false),
                    assetId = table.Column<int>(nullable: false),
                    CurrentQuntity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.id);
                    table.ForeignKey(
                        name: "FK_stocks_Asset_assetId",
                        column: x => x.assetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_Store_storeId",
                        column: x => x.storeId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_Unit_unitId",
                        column: x => x.unitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_OperaterId",
                table: "orders",
                column: "OperaterId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_TakerId1",
                table: "orders",
                column: "TakerId1");

            migrationBuilder.CreateIndex(
                name: "IX_orders_assetId",
                table: "orders",
                column: "assetId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_storeId",
                table: "orders",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_unitId",
                table: "orders",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_assetId",
                table: "stocks",
                column: "assetId");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_storeId",
                table: "stocks",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_unitId",
                table: "stocks",
                column: "unitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "stocks");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
