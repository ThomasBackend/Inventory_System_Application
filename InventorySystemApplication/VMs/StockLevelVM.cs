using InventorySystemApplication.Models;
namespace InventorySystemApplication.VMs
{
    public class StockLevelVM
    {
        public int Id { get; set; }
        public int Quantity_In_Stock { get; set; }
        public int Product_Id { get; set; }
        public int Warehouse_Id { get; set; }

        public Product Products {get; set;}
        public Warehouse Warehouses {get; set;}
    }
}
