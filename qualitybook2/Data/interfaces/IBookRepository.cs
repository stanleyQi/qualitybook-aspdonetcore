using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Data.interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
        IEnumerable<Book> PreferredBooks { get;}

        IQueryable<Book> SearchedBooks(string searchKey,int searchCriteria,int categoryId);
    }
}
