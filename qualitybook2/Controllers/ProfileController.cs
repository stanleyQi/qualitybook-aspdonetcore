using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qualitybook2.Controllers.Admin;
using qualitybook2.Data;
using qualitybook2.Models;
using qualitybook2.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace qualitybook2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly QualityBookDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, QualityBookDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: /<controller>/
        [Authorize]
        public async Task<IActionResult> Index()
        {
            Customer curUser = new Customer(); 
            CustomersController.ConvertFromAppUserToCustomer(await _userManager.GetUserAsync(HttpContext.User),ref curUser);
            List<Order> orders = _context.Orders.Where(order => order.CustomerId == curUser.CustomerId)
            .OrderByDescending(order => order.OrderPlacedDateTime)
            .ToList();

            CustomersAndOrdersAndOrderDetailsViewModel vm = new CustomersAndOrdersAndOrderDetailsViewModel();
            vm.Orders = orders;
            vm.CustomerCurrent= curUser;
            vm.CustomerIdCurrent = curUser.CustomerId;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetailsOfOrder(string customerid, string orderId)
        {
            // retrive customers, orders and orderdetails of the order specified
            Customer curUser = new Customer();
            CustomersController.ConvertFromAppUserToCustomer(await _userManager.GetUserAsync(HttpContext.User), ref curUser);

            List<Order> orders = _context.Orders.Where(order => order.CustomerId == customerid)
                .OrderByDescending(order => order.OrderPlacedDateTime)
                .ToList();

            List<OrderDetail> orderDetails = _context.OrderDetails
                .Where(orderDetail => orderDetail.OrderId.ToString() == orderId)
                .OrderByDescending(orderdetail => orderdetail.BookId)
                .Include(od => od.book).ThenInclude(b => b.Category)
                .Include(b => b.book).ThenInclude(b => b.Supplier)
                .ToList();

            CustomersAndOrdersAndOrderDetailsViewModel vm = new CustomersAndOrdersAndOrderDetailsViewModel();
            vm.CustomerCurrent = curUser;
            vm.Orders = orders;
            vm.OrderDetails = orderDetails;

            vm.CustomerIdCurrent = customerid;
            vm.OrderIdCurrent = orderId;

            return View("Views/Profile/Index.cshtml", vm);
        }
    }
}
