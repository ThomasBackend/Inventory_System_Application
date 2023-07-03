using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystemApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Product_Name { get; set; }
        [Required]
        public DateTime Manufacturing_Date { get; set;}
        [Required]
        public DateTime Expiry_Date { get; set;}
        [Required]
        public int Category_Id { get; set;}


       
    }
}
