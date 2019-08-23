using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GST { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
