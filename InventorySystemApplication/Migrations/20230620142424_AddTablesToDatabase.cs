using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventorySystemApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDatabase : Migration
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
                name: "UsersTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(name: "User_Name", type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(name: "User_Email", type: "nvarchar(max)", nullable: false),
                    UserTelephone = table.Column<int>(name: "User_Telephone", type: "int", nullable: false),
                    WarehouseId = table.Column<int>(name: "Warehouse_Id", type: "int", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriesTable");

            migrationBuilder.DropTable(
                name: "ProductsTable");

            migrationBuilder.DropTable(
                name: "StockInTable");

            migrationBuilder.DropTable(
                name: "StockLevelTable");

            migrationBuilder.DropTable(
                name: "StockTransfersTable");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropTable(
                name: "WarehouseProductTable");

            migrationBuilder.DropTable(
                name: "WarehousesTable");
        }
    }
}
