using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.ViewModels
{
    public class CustomersAndOrdersAndOrderDetailsViewModel
    {
        public string CustomerIdCurrent { get; set; }
        public string OrderIdCurrent { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
