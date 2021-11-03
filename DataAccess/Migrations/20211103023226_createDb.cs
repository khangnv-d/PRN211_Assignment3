using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class createDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberObject",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: true),
                    City = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: true),
                    Country = table.Column<string>(type: "VARCHAR(15)", maxLength: 15, nullable: true),
                    Password = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberObject", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "ProductObject",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "VARCHAR(40)", maxLength: 40, nullable: true),
                    Weight = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    UnitsInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductObject", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "OrderObject",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Freight = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderObject", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderObject_MemberObject_MemberId",
                        column: x => x.MemberId,
                        principalTable: "MemberObject",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailObject",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailObject", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetailObject_OrderObject_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderObject",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailObject_ProductObject_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductObject",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailObject_OrderId",
                table: "OrderDetailObject",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailObject_ProductId",
                table: "OrderDetailObject",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderObject_MemberId",
                table: "OrderObject",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailObject");

            migrationBuilder.DropTable(
                name: "OrderObject");

            migrationBuilder.DropTable(
                name: "ProductObject");

            migrationBuilder.DropTable(
                name: "MemberObject");
        }
    }
}
