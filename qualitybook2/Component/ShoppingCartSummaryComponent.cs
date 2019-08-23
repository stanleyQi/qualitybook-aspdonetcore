using Microsoft.AspNetCore.Mvc;
using qualitybook2.Models;
using qualitybook2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qualitybook2.Component
{
    public class ShoppingCartSummaryComponent:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummaryComponent(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = shoppingCartItems;

            var vm = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                SubTotal = _shoppingCart.GetShoppingCartTotal().Item1,
                GST = _shoppingCart.GetShoppingCartTotal().Item2,
                GrandTotal = _shoppingCart.GetShoppingCartTotal().Item3
            };
            return View(vm);
        }
    }
}
