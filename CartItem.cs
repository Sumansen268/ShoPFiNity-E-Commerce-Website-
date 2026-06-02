using System.ComponentModel.DataAnnotations;

namespace WebProject_ECommerce.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int ProductId { get; set; }

        public int Price { get; set; }

        [MaxLength(30)]

        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        public int Quantity { get; set; }

        public Product product { get; set; }
    }
}
