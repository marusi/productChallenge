using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazor.Infrastructure.Migrations
{
    public partial class ProductSku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductSkus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkuValueId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSkus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSkus_SkuValues_SkuValueId",
                        column: x => x.SkuValueId,
                        principalTable: "SkuValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSkus_SkuValueId",
                table: "ProductSkus",
                column: "SkuValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSkus");
        }
    }
}
