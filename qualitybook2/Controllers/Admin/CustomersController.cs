using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qualitybook2.Data;
using qualitybook2.Models;

namespace qualitybook2.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CustomersController : Controller
    {
        private string ViewPath = "Views/Admin/Customers/";

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CustomersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Books
        public ViewResult Index()
        {
            return View(ViewPath + "Index.cshtml",new Customer[] { new Customer { CustomerId = 1, CustomerName = "mock", Email = "mock@gmail.com", PhoneNumberHome = "1111", Address = "dsfdsfdf", LockoutEnabled = 1 } });
        }
    }
}
