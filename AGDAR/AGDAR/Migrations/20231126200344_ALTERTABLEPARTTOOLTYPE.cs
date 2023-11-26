using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGDAR.Migrations
{
    /// <inheritdoc />
    public partial class ALTERTABLEPARTTOOLTYPE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToolType",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToolType",
                table: "Parts");
        }
    }
}
