using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySystemApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(name: "Category_Name", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(name: "Product_Name", type: "nvarchar(max)", nullable: false),
                    ManufacturingDate = table.Column<DateTime>(name: "Manufacturing_Date", type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(name: "Expiry_Date", type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(name: "Category_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockInTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    QuantityIn = table.Column<int>(name: "Quantity_In", type: "int", nullable: false),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false),
                    DateIn = table.Column<DateTime>(name: "Date_In", type: "datetime2", nullable: false),
                    UserId = table.Column<int>(name: "User_Id", type: "int", nullable: false),
                    DocumentNumber = table.Column<int>(name: "Document_Number", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockLevelTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityInStock = table.Column<int>(name: "Quantity_In_Stock", type: "int", nullable: false),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockLevelTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTransfersTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    WarehouseFromId = table.Column<int>(name: "Warehouse_FromId", type: "int", nullable: false),
                    WarehouseToId = table.Column<int>(name: "Warehouse_ToId", type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(name: "User_Id", type: "int", nullable: false),
                    TransferDate = table.Column<DateTime>(name: "Transfer_Date", type: "datetime2", nullable: false),
                    DocumentNumber = table.Column<int>(name: "Document_Number", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransfersTable", x => x.Id);
                });

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
                    UserId = table.Column<int>(name: "User_Id", type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsersTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(name: "User_Name", type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(name: "User_Email", type: "nvarchar(max)", nullable: false),
                    UserTelephone = table.Column<int>(name: "User_Telephone", type: "int", nullable: false),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProductTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(name: "Product_Id", type: "int", nullable: false),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProductTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehousesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(name: "Phone_Number", type: "int", nullable: false),
                    EmailAddress = table.Column<string>(name: "Email_Address", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehousesTable", x => x.Id);
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
                name: "CategoriesTable");

            migrationBuilder.DropTable(
                name: "StockInTable");

            migrationBuilder.DropTable(
                name: "StockLevelTable");

            migrationBuilder.DropTable(
                name: "StockTransfersTable");

            migrationBuilder.DropTable(
                name: "SystemUsersTable");

            migrationBuilder.DropTable(
                name: "UserProductVM");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropTable(
                name: "WarehouseProductTable");

            migrationBuilder.DropTable(
                name: "ProductsTable");

            migrationBuilder.DropTable(
                name: "WarehousesTable");
        }
    }
}
