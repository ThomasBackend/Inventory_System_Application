using System.ComponentModel.DataAnnotations;
namespace InventorySystemApplication.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set;}
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Phone_Number { get; set; }
        [Required]
        public string Email_Address { get; set; }
    }
}
