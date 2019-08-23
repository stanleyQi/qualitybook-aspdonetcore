using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Please enter your first name.")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Please enter")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Please enter")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter")]
        public string Email { get; set; }

        public decimal OrderTotalPrice { get; set; }
        public DateTime OrderPlacedDateTime { get; set; }
    }
}
