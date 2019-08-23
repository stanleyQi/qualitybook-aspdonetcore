using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Author { get; set; }
        public int PreferredFlag { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
