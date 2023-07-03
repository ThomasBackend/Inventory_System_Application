using InventorySystemApplication.Models;
namespace InventorySystemApplication.VMs
{
    public class StockTfVM
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Warehouse_FromId { get; set; }
        public int Warehouse_ToId { get; set; }
        public int Quantity { get; set; }
        public int User_Id { get; set; }
        public DateTime Transfer_Date { get; set; }
        public int Document_Number { get; set; }

        public Product Products {get; set;}
        public Warehouse WarehousesTo {get; set;}
        public Warehouse WarehousesFrom {get; set;}
        public User Users {get; set;}
    }
}
