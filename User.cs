using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject_ECommerce.Models
{
    public class User
    {

        [MaxLength(20)]
        public string Fname { get; set; }
        [MaxLength(20)]
        public string Lname { get; set; }

        [Key]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string Re_Password { get; set; }

        public string SecurityQuestion { get; set; }
        [MaxLength(100)]
        public string Answer { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        [MaxLength(10)]
        public string Contact_Number { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }
    }
}
