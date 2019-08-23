using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Data.mocks
{
    public class DbInitializer
    {
        public static void Initialize(QualityBookDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Books.Any())
            {
                return; // DB has been seeded
            }


            var categories = new Category[]
            {
                //new Category{ CategoryId=1,CategoryName = "dbcat1"},
                //new Category{ CategoryId=2,CategoryName = "dbcat2"},
                //new Category{ CategoryId=3,CategoryName = "dbcat3"}
                new Category{ CategoryName = "dbcat1"},
                new Category{ CategoryName = "dbcat2"},
                new Category{ CategoryName = "dbcat3"}
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
            {
                //new Supplier{ SupplierId = 1,SupplierName = "dbsup1"},
                //new Supplier{ SupplierId = 2,SupplierName = "dbsup2"},
                //new Supplier{ SupplierId = 3,SupplierName = "dbsup3"}
                new Supplier{ SupplierName = "dbsup1"},
                new Supplier{ SupplierName = "dbsup2"},
                new Supplier{ SupplierName = "dbsup3"}
            };
            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            var books = new Book[]
            {
                new Book{ BookName = "dbbook1",Price = 1.11M,CategoryId=1,SupplierId=1,ShortDescription="This is a very good book.",Author="LiQi",PreferredFlag=1,ImageUrl="book1.jpg"},
                new Book{ BookName = "dbbook2",Price = 2.22M,CategoryId=2,SupplierId=2,ShortDescription="This is a very good book.",Author="LiQi",PreferredFlag=2,ImageUrl="book2.jpg"},
                new Book{ BookName = "dbbook3",Price = 3.33M,CategoryId=3,SupplierId=3,ShortDescription="This is a very good book.",Author="LiQi",PreferredFlag=3,ImageUrl="book3.jpg"}
            };
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();

            
        }
    }
}
