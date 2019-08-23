using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.ViewModels
{
    public class PreferredBookListViewModel
    {
        public IEnumerable<Book> PreferredBooks { get; set; }
    }
}
