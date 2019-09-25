using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qualitybook2.Data;
using qualitybook2.Models;
using qualitybook2.ViewModels;

namespace qualitybook2.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly QualityBookDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, QualityBookDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            // retrive customers for initializing dispaly
            List<Customer> customers = CustomersController.ConvertFromAppUsersToCustomers(_userManager.Users.ToList());
            CustomersAndOrdersAndOrderDetailsViewModel vm = new CustomersAndOrdersAndOrderDetailsViewModel();

            vm.Customers = customers;

            return View("Views/Admin/Index.cshtml", vm);
        }

        [HttpGet]
        public IActionResult GetOrdersOfCustomer(string CustomerId)
        {
            // retrive customers and orders of the customer specified
            List<Customer> customers = CustomersController.ConvertFromAppUsersToCustomers(_userManager.Users.ToList());
            List<Order> orders = _context.Orders.Where(order => order.CustomerId == CustomerId)
                .OrderByDescending(order => order.OrderPlacedDateTime)
                .ToList();
            CustomersAndOrdersAndOrderDetailsViewModel vm = new CustomersAndOrdersAndOrderDetailsViewModel();
            vm.Customers = customers;
            vm.Orders = orders;
            vm.CustomerIdCurrent = CustomerId;

            return View("Views/Admin/Index.cshtml", vm);
        }

        [HttpGet]
        public IActionResult GetOrderDetailsOfOrder(string customerid, string orderId)
        {
            // retrive customers, orders and orderdetails of the order specified
            List<Customer> customers = CustomersController.ConvertFromAppUsersToCustomers(_userManager.Users.ToList());
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
            vm.Customers = customers;
            vm.Orders = orders;
            vm.OrderDetails = orderDetails;

            vm.CustomerIdCurrent = customerid;
            vm.OrderIdCurrent = orderId;

            return View("Views/Admin/Index.cshtml", vm);
        }

        [HttpGet]
        public IActionResult changeStatus(string customerid, string orderId, string state)
        {
            List<Order> orders1 = _context.Orders.Where(order => order.OrderId.ToString() == orderId)
                .OrderByDescending(order => order.OrderPlacedDateTime)
                .ToList();
            Order currentOrder = orders1[0];
            currentOrder.State = (state == "waiting" ? "shipped" : "waiting");
            _context.Orders.Update(currentOrder);

            // retrive customers, orders and orderdetails of the order specified
            List<Customer> customers = CustomersController.ConvertFromAppUsersToCustomers(_userManager.Users.ToList());
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
            vm.Customers = customers;
            vm.Orders = orders;
            vm.OrderDetails = orderDetails;

            vm.CustomerIdCurrent = customerid;
            vm.OrderIdCurrent = orderId;

            return View("Views/Admin/Index.cshtml", vm);
        }
    }
}