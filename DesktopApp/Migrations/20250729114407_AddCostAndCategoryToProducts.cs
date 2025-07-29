using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesktopApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCostAndCategoryToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "COST",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductCategory",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COST",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "Products");
        }
    }
}
