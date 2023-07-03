using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySystemApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemUsersToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UsersTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SystemUsersTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(name: "User_Name", type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(name: "User_Email", type: "nvarchar(max)", nullable: false),
                    UserTelephone = table.Column<int>(name: "User_Telephone", type: "int", nullable: false),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsersTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProductVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    WarehousesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProductVM_ProductsTable_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "ProductsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProductVM_WarehousesTable_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "WarehousesTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProductVM_ProductsId",
                table: "UserProductVM",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductVM_WarehousesId",
                table: "UserProductVM",
                column: "WarehousesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemUsersTable");

            migrationBuilder.DropTable(
                name: "UserProductVM");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "UsersTable");
        }
    }
}
