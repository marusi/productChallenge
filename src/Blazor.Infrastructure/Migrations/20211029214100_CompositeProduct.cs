using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazor.Infrastructure.Migrations
{
    public partial class CompositeProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompositeProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositeProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CombinedProducts",
                columns: table => new
                {
                    ProductSkuId = table.Column<int>(type: "int", nullable: false),
                    CompositeProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombinedProducts", x => new { x.CompositeProductId, x.ProductSkuId });
                    table.ForeignKey(
                        name: "FK_CombinedProducts_CompositeProducts_CompositeProductId",
                        column: x => x.CompositeProductId,
                        principalTable: "CompositeProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombinedProducts_ProductSkus_ProductSkuId",
                        column: x => x.ProductSkuId,
                        principalTable: "ProductSkus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CombinedProducts_ProductSkuId",
                table: "CombinedProducts",
                column: "ProductSkuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CombinedProducts");

            migrationBuilder.DropTable(
                name: "CompositeProducts");
        }
    }
}
