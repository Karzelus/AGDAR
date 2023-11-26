using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGDAR.Migrations
{
    /// <inheritdoc />
    public partial class ALTERTablePARTType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartProduct");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Parts");

            migrationBuilder.CreateTable(
                name: "PartProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartProduct_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartProduct_PartId",
                table: "PartProduct",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_PartProduct_ProductId",
                table: "PartProduct",
                column: "ProductId");
        }
    }
}
