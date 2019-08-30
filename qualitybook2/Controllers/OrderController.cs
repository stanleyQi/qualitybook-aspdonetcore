using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qualitybook2.Data.interfaces;
using qualitybook2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace qualitybook2.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        //// GET: /<controller>/
        //[Authorize]
        //public IActionResult Checkout()
        //{
        //    return View();
        //}


        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        //[HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count==0)
            {
                ModelState.AddModelError("", "Your shopping cart is empty, add some books frist");
                TempData["empty"] = "Your shopping cart is empty, add some books frist";
            }

            if (ModelState.IsValid)
            {
                // create a new order
                Order newOrder = new Order();
                var currentUser = await GetCurrentUserAsync();
                newOrder.CustomerId = currentUser?.Id.ToString();

                _orderRepository.CreateOrder(newOrder);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult CheckoutComplete()
        {
            //ViewData["words"] = "Thanks for your purchase.";
            return View();
        }
    }
}
