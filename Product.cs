using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject_ECommerce.Models
{
    public class Product
    {

        [Key]

        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }
        public int Price { get; set; }

        [MaxLength(20)]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        [MaxLength(20)]

        [NotMapped]
        public string CategoryName { get; set; }

        public string PictureName { get; set; }
        [NotMapped]
        public IFormFile? PictureFile { get; set; }



    }
}
