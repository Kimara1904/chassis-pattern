using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCountFromAuthorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
