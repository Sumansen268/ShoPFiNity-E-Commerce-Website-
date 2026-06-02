using System.ComponentModel.DataAnnotations;

namespace WebProject_ECommerce.Models
{
    public class Order
    {

        [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }


        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }


        //[DataType(DataType.Date)]
        public DateTime DOP { get; set; } = DateTime.Now;


        public int Quantity { get; set; }

        public int Total_Amount { get; set; }

    }

}
