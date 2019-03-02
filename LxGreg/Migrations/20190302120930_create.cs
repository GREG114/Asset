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
                name: "assets",
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
                    table.PrimaryKey("PK_assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "managers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoreName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    storeId = table.Column<int>(nullable: false),
                    unitId = table.Column<int>(nullable: false),
                    assetId = table.Column<int>(nullable: false),
                    Quntity = table.Column<int>(nullable: false),
                    TakerId = table.Column<string>(nullable: true),
                    OperaterId = table.Column<string>(nullable: true),
                    Mark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_managers_OperaterId",
                        column: x => x.OperaterId,
                        principalTable: "managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_managers_TakerId",
                        column: x => x.TakerId,
                        principalTable: "managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_assets_assetId",
                        column: x => x.assetId,
                        principalTable: "assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_stores_storeId",
                        column: x => x.storeId,
                        principalTable: "stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_units_unitId",
                        column: x => x.unitId,
                        principalTable: "units",
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
                        name: "FK_stocks_assets_assetId",
                        column: x => x.assetId,
                        principalTable: "assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_stores_storeId",
                        column: x => x.storeId,
                        principalTable: "stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_units_unitId",
                        column: x => x.unitId,
                        principalTable: "units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_OperaterId",
                table: "orders",
                column: "OperaterId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_TakerId",
                table: "orders",
                column: "TakerId");

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
                name: "managers");

            migrationBuilder.DropTable(
                name: "assets");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "units");
        }
    }
}
