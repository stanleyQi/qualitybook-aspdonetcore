using qualitybook2.Data.interfaces;
using qualitybook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly QualityBookDbContext _qualityBookDbContext;

        public OrderRepository(ShoppingCart shoppingCart, QualityBookDbContext  qualityBookDbContext)
        {
            _shoppingCart = shoppingCart;
            _qualityBookDbContext = qualityBookDbContext;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlacedDateTime = DateTime.Now;
            _qualityBookDbContext.Orders.Add(order);

            foreach (var item in _shoppingCart.ShoppingCartItems)
            {
                var OrderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    BookId = item.Book.BookId,
                    OrderId = order.OrderId,
                    Price = item.Book.Price
                };
                _qualityBookDbContext.OrderDetails.Add(OrderDetail);
            }

            _qualityBookDbContext.SaveChanges();
        }
    }
}
