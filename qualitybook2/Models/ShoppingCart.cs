using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using qualitybook2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private readonly QualityBookDbContext _qualityBookDbContext;

        public ShoppingCart(QualityBookDbContext qualityBookDbContext)
        {
            _qualityBookDbContext = qualityBookDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<QualityBookDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Book book, int amount)
        {
            var shoppingCartItem = _qualityBookDbContext.ShoppingCartItems.SingleOrDefault(
                sci => sci.Book.BookId==book.BookId && sci.ShoppingCartId == ShoppingCartId
                );

            if (shoppingCartItem==null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1
                };
                _qualityBookDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _qualityBookDbContext.SaveChanges();
        }

        public int RemoveFromCart(Book book,string flg)
        {
            var shoppingCartItem = _qualityBookDbContext.ShoppingCartItems.SingleOrDefault(
               sci => sci.Book.BookId == book.BookId && sci.ShoppingCartId == ShoppingCartId
               );

            var removeResult = 0;
            if (shoppingCartItem!=null)
            {
                if (shoppingCartItem.Amount>1 && !flg.Equals("delete"))
                {
                    shoppingCartItem.Amount--;
                    removeResult = shoppingCartItem.Amount;
                }
                else
                {
                    _qualityBookDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _qualityBookDbContext.SaveChanges();
            return removeResult;
        }

        public  List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (
                    ShoppingCartItems = _qualityBookDbContext.ShoppingCartItems.Where(
                            sci => sci.ShoppingCartId == ShoppingCartId).Include(sci => sci.Book).ToList());
        }

        public void ClearCart()
        {
            var shoppingCartItemsForClearing = _qualityBookDbContext.ShoppingCartItems.Where(
                sci => sci.ShoppingCartId == ShoppingCartId);

            _qualityBookDbContext.ShoppingCartItems.RemoveRange(shoppingCartItemsForClearing);

            _qualityBookDbContext.SaveChanges();
        }

        public Tuple<decimal,decimal,decimal> GetShoppingCartTotal()
        {
            decimal subTotal = _qualityBookDbContext.ShoppingCartItems.Where(
                sci => sci.ShoppingCartId == ShoppingCartId).Select(
                sci => sci.Book.Price * sci.Amount).Sum();

            var GST = subTotal * 0.15M;
            var grandTotal = subTotal + GST;

            return new Tuple<decimal, decimal, decimal>(subTotal,GST ,grandTotal);
        }
    }
}
