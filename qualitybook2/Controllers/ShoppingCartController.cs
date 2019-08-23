using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qualitybook2.Data.interfaces;
using qualitybook2.Models;
using qualitybook2.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace qualitybook2.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IBookRepository bookRepository, ShoppingCart shoppingCart)
        {
            _bookRepository = bookRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var cartItems = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = cartItems;

            var vm = new ShoppingCartViewModel {
                ShoppingCart = _shoppingCart,
                SubTotal = _shoppingCart.GetShoppingCartTotal().Item1,
                GST = _shoppingCart.GetShoppingCartTotal().Item2,
                GrandTotal = _shoppingCart.GetShoppingCartTotal().Item3
             };

            return View(vm);
        }

        public RedirectToActionResult AddToCart(int bookId)
        {
            var addedBook = _bookRepository.Books.FirstOrDefault(b => b.BookId == bookId);
            if (addedBook != null)
            {
                _shoppingCart.AddToCart(addedBook, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int bookId,string flg="none")
        {
            var removedBook = _bookRepository.Books.FirstOrDefault(b => b.BookId == bookId);
            if (removedBook != null)
            {
                _shoppingCart.RemoveFromCart(removedBook,flg);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }

        public RedirectToActionResult CheckOut()
        {
            return RedirectToAction("Index");
        }



    }
}
