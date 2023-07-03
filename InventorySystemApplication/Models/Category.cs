using System.ComponentModel.DataAnnotations;
namespace InventorySystemApplication.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Category_Name { get; set; }

       
    }
}
