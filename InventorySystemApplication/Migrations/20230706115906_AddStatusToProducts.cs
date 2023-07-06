using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySystemApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ProductsTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductsTable");
        }
    }
}
