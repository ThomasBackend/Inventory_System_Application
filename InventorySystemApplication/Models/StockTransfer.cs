using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystemApplication.Models
{
    public class StockTransfer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public int Warehouse_FromId { get; set; }
        [Required]
        public int Warehouse_ToId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int User_Id { get; set; }
        [Required]
        public DateTime Transfer_Date { get; set; }
        [Required]
        public int Document_Number { get; set; }





     
    }
}
