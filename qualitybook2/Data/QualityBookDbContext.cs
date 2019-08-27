using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qualitybook2.Models;

namespace qualitybook2.Data
{
    public class QualityBookDbContext : IdentityDbContext<ApplicationUser>
    {
        public QualityBookDbContext(DbContextOptions<QualityBookDbContext> options): base(options)
        {
           
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        //public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(builder);
            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Supplier>().ToTable("Suppliers");
            builder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItems");

            //builder.Entity<Customer>().ToTable("AspNetUsers");
        }
    }
}
