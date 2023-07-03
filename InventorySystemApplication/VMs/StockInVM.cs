using InventorySystemApplication.Models;
namespace InventorySystemApplication.VMs
{
    public class StockInVM
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity_In { get; set; }
        public int Warehouse_Id { get; set; }
        public DateTime Date_In { get; set; }
        public int User_Id { get; set; }
        public int Document_Number { get; set; }

        public Product Products {get; set;}
        public Warehouse Warehouses {get; set;}
        public User Users {get; set;}
    }
}
