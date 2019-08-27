using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Models
{
    //[Table("AspNetUsers")]
    public class Customer
    {
        //[Column("Id")]
        public string CustomerId { get; set; }

        //[Column("UserName")]
        public string CustomerName { get; set; }

        [Display(Name = "Home Number")]
        public string PhoneNumberHome { get; set; }

        [Display(Name = "Work Number")]
        public string PhoneNumberWork { get; set; }

        [Display(Name ="Mobile")]
        public string PhoneNumberMobile { get; set; }

        //[Column("Email")]
        public string Email { get; set; }

        //[Column("Address")]
        public string Address { get; set; }

        [Display(Name = "Locked due date")]
        public DateTimeOffset? LockoutEnd { get; set; }

    }
}
