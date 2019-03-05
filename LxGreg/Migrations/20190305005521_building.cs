using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LxGreg.Migrations
{
    public partial class building : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    UnitName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_units", x => x.Id);
                    table.UniqueConstraint("AK_units_UnitName", x => x.UnitName);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    ItemNumber = table.Column<string>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Mark = table.Column<string>(nullable: true),
                    ItemShortNumber = table.Column<string>(nullable: true),
                    storeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.ItemNumber);
                    table.ForeignKey(
                        name: "FK_items_stores_storeId",
                        column: x => x.storeId,
                        principalTable: "stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Mark = table.Column<string>(nullable: true),
                    take = table.Column<bool>(nullable: false),
                    unitId = table.Column<int>(nullable: false),
                    itemItemNumber = table.Column<string>(nullable: true),
                    TakerId = table.Column<string>(nullable: true),
                    OperaterId = table.Column<string>(nullable: true)
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
                        name: "FK_orders_items_itemItemNumber",
                        column: x => x.itemItemNumber,
                        principalTable: "items",
                        principalColumn: "ItemNumber",
                        onDelete: ReferentialAction.Restrict);
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
                    itemItemNumber = table.Column<string>(nullable: true),
                    unitId = table.Column<int>(nullable: false),
                    CurrentQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.id);
                    table.ForeignKey(
                        name: "FK_stocks_items_itemItemNumber",
                        column: x => x.itemItemNumber,
                        principalTable: "items",
                        principalColumn: "ItemNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stocks_units_unitId",
                        column: x => x.unitId,
                        principalTable: "units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_storeId",
                table: "items",
                column: "storeId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_OperaterId",
                table: "orders",
                column: "OperaterId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_TakerId",
                table: "orders",
                column: "TakerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_itemItemNumber",
                table: "orders",
                column: "itemItemNumber");

            migrationBuilder.CreateIndex(
                name: "IX_orders_unitId",
                table: "orders",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_itemItemNumber",
                table: "stocks",
                column: "itemItemNumber");

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
                name: "items");

            migrationBuilder.DropTable(
                name: "units");

            migrationBuilder.DropTable(
                name: "stores");
        }
    }
}
