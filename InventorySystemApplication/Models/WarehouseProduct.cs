using System.ComponentModel.DataAnnotations;
namespace InventorySystemApplication.Models
{
    public class WarehouseProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public int Warehouse_Id { get; set; }
    }
}
