using System.ComponentModel.DataAnnotations;

namespace WebProject_ECommerce.Models
{
    public class Category
    {

        [Key]
        public int CatId { get; set; }

        [MaxLength(20)]
        public string CatName { get; set; }
        public int CategoryId { get; set; }
    }
}
