using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InventorySystemApplication.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string User_Email { get; set;}
        [Required]
        public int User_Telephone { get; set;}
        [Required]
        public int Warehouse_Id { get; set;}
        [Required]
        public string Password { get; set; }


       
    }
}
