using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string PhoneNumberHome { get; set; }
        public string PhoneNumberWork { get; set; }
        public string PhoneNumberMobile { get; set; }
        public string Email { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
