using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystemApplication.Models
{
    public class StockLevel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity_In_Stock { get; set; }
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public int Warehouse_Id { get; set; }

        
       
    }
}
