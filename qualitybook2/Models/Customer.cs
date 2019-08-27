using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Models
{
    //[Table("AspNetUsers")]
    public class Customer
    {
        //[Column("Id")]
        public int CustomerId { get; set; }

        //[Column("UserName")]
        public string CustomerName { get; set; }

        //[Column("PhoneNumber")]
        public string PhoneNumberHome { get; set; }

        //[Column("PhoneNumber")]
        public string PhoneNumberWork { get; set; }

        //[Column("PhoneNumber")]
        public string PhoneNumberMobile { get; set; }

        //[Column("Email")]
        public string Email { get; set; }

        //[Column("Address")]
        public string Address { get; set; }

        //[Column("LockoutEnabled")]
        public int LockoutEnabled { get; set; }

    }
}
