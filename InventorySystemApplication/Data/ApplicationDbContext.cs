using InventorySystemApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using InventorySystemApplication.VMs;

namespace InventorySystemApplication.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> CategoriesTable { get; set; }
        public DbSet<Product> ProductsTable { get; set; }
        public DbSet<StockIn> StockInTable { get; set; }
        public DbSet<StockLevel> StockLevelTable { get; set; }
        public DbSet<StockTransfer> StockTransfersTable { get; set; }
        public DbSet<SystemUser> SystemUsersTable { get; set; }
        public DbSet<User> UsersTable { get; set; }
        public DbSet<Warehouse> WarehousesTable { get; set; }
        public DbSet<WarehouseProduct> WarehouseProductTable { get; set; }
        public DbSet<InventorySystemApplication.VMs.UserProductVM>? UserProductVM { get; set; }
       


    }
}
