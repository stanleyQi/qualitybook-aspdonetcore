using Microsoft.EntityFrameworkCore;
using qualitybook2.Data.interfaces;
using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly QualityBookDbContext _qualityBookDbContext;
        public BookRepository(QualityBookDbContext qualityBookDbContext)
        {
            _qualityBookDbContext = qualityBookDbContext;
        }

        public IEnumerable<Book> Books {
            get => _qualityBookDbContext.Books.Include(b => b.Category).Include(b => b.Supplier);
         }

        public IEnumerable<Book> PreferredBooks => _qualityBookDbContext.Books
            .Where<Book>(b => b.PreferredFlag == 1 || b.PreferredFlag == 2 || b.PreferredFlag == 3)
            .Include(b => b.Category)
            .Include(b => b.Supplier);

        public IEnumerable<Book> SearchedBooks(string searchKey="", int searchCriteria=1,int categoryId = 0)
        {
            if (searchCriteria == 1&& categoryId!=0)
            {
                return _qualityBookDbContext.Books
                .Where<Book>(b => b.BookName.Contains(searchKey)&&b.CategoryId== categoryId)
                .Include(b => b.Category);
            }
            else if (searchCriteria == 2&& categoryId!=0)
            {
                return _qualityBookDbContext.Books
                .Where<Book>(b => b.Price.ToString().Equals(searchKey) && b.CategoryId == categoryId)
                .Include(b => b.Category);
            }
            else if (searchCriteria == 1 && categoryId == 0)
            {
                return _qualityBookDbContext.Books
                .Where<Book>(b => b.BookName.Contains(searchKey))
                .Include(b => b.Category);
            }
            else if (searchCriteria == 2 && categoryId == 0)
            {
                return _qualityBookDbContext.Books
                .Where<Book>(b => b.Price.ToString().Equals(searchKey))
                .Include(b => b.Category);
            }
            else
            {
                return null;
            }
        }
    }
}
