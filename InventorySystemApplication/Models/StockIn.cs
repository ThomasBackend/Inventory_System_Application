using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystemApplication.Models
{
    public class StockIn
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public int Quantity_In { get; set; }
        [Required]
        public int Warehouse_Id { get; set; }
        [Required]
        public DateTime Date_In { get; set; }
        [Required]
        public int User_Id { get; set; }
        [Required]
        public int Document_Number { get; set; }

        
    }
}
