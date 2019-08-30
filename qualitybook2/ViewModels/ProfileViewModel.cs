using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.ViewModels
{
    public class ProfileViewModel
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Order> Orders { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
