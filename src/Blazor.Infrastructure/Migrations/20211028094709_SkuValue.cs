using Microsoft.EntityFrameworkCore.Migrations;

namespace Blazor.Infrastructure.Migrations
{
    public partial class SkuValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkuValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionValueId = table.Column<int>(type: "int", nullable: false),
                    SkuValueName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkuValues_OptionValues_OptionValueId",
                        column: x => x.OptionValueId,
                        principalTable: "OptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkuValues_OptionValueId",
                table: "SkuValues",
                column: "OptionValueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkuValues");
        }
    }
}
