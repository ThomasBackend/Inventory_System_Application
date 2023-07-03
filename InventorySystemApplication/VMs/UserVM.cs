using InventorySystemApplication.Models;
namespace InventorySystemApplication.VMs
{
    public class UserVM
    {
        public int Id { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set;}
        public int User_Telephone { get; set;}
        public int Warehouse_Id { get; set;}
        public string Password { get; set; }

        public Warehouse Warehouses {get; set;}
    }
}
