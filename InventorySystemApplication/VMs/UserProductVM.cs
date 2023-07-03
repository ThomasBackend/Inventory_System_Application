using InventorySystemApplication.Models;
namespace InventorySystemApplication.VMs
{
    public class UserProductVM
    {
        public int Id { get; set; }
        public int Warehouse_Id { get; set; }
        public int Product_Id { get; set; }

        public Warehouse Warehouses { get; set; }

        public Product Products { get; set; }
    }
}
