using System.ComponentModel.DataAnnotations;

namespace WebProject_ECommerce.Models
{
    public class Admin
    {
        [Key]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
