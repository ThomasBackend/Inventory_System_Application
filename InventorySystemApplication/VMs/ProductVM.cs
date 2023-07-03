using InventorySystemApplication.Models;
namespace InventorySystemApplication.VMs
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public DateTime Manufacturing_Date { get; set;}
        public DateTime Expiry_Date { get; set;}
        public int Category_Id { get; set;}

        public Category Categories {get; set;}
    }
}
