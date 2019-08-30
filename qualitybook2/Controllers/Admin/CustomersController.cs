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

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CustomersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Books
        public ViewResult Index()
        {
            return View(ViewPath + "Index.cshtml", ConvertFromAppUsersToCustomers(_userManager.Users.ToList()));
        }

        

        //user.LockoutEnabled = true;
        //user.LockoutEndDateUtc = DateTime.UtcNow.AddMinutes(42);
        //await userManager.UpdateAsync(user);
        [HttpGet]
        public async Task<IActionResult> Lockout(string id)
        {
            
            try
            {
                ApplicationUser appUser = _userManager.Users.Where(user => user.Id == id).FirstOrDefault();
                appUser.LockoutEnabled = true;
                appUser.LockoutEnd = DateTime.UtcNow.AddMinutes(10);
                await _userManager.UpdateAsync(appUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Unlock(string id)
        {

            try
            {
                ApplicationUser appUser = _userManager.Users.Where(user => user.Id == id).FirstOrDefault();
                appUser.LockoutEnabled = true;
                appUser.LockoutEnd = null;
                appUser.NormalizedEmail = null;
                await _userManager.UpdateAsync(appUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public static List<Customer> ConvertFromAppUsersToCustomers(List<ApplicationUser> ListIdnUser)
        {

            var Customers = new List<Customer>();

            foreach (ApplicationUser user in ListIdnUser)
            {
                Customer customer = new Customer();
                customer.CustomerId = user.Id;
                customer.CustomerName = user.UserName;
                customer.PhoneNumberHome = user.PhoneNumber;
                customer.PhoneNumberWork = user.PhoneNumber;
                customer.PhoneNumberMobile = user.PhoneNumber;
                customer.Email = user.Email;
                customer.Address = user.Address;
                customer.LockoutEnd = user.LockoutEnd;
                Customers.Add(customer);
            }
            return Customers;
        }

        private static List<ApplicationUser> ConvertFromCustomersToAppUsers(List<Customer> ListCustomer)
        {

            var Users = new List<ApplicationUser>();

            foreach (Customer customer in ListCustomer)
            {
                ApplicationUser user = new ApplicationUser();
                user.Id = customer.CustomerId;
                user.UserName = customer.CustomerName;
                user.PhoneNumber = customer.PhoneNumberHome;
                user.PhoneNumber = customer.PhoneNumberWork;
                user.PhoneNumber = customer.PhoneNumberMobile;
                user.Email = customer.Email;
                user.Address = customer.Address;
                user.LockoutEnd = customer.LockoutEnd;
                Users.Add(user);
            }
            return Users;
        }

        private static void ConvertFromAppUserToCustomer(ApplicationUser appUser1, ref Customer customer)
        {

            customer.CustomerId = appUser1.Id;
            customer.CustomerName = appUser1.UserName;
            customer.PhoneNumberHome = appUser1.PhoneNumber;
            customer.PhoneNumberWork = appUser1.PhoneNumber;
            customer.PhoneNumberMobile = appUser1.PhoneNumber;
            customer.Email = appUser1.Email;
            customer.Address = appUser1.Address;
            //customer.LockoutEnd = IdnUser.LockoutEnd;
        }

        private static void ConvertFromCustomerToAppUser(Customer Customer,ref ApplicationUser appUser1)
        {
            appUser1.Id = Customer.CustomerId;
            appUser1.UserName = Customer.CustomerName;
            appUser1.PhoneNumber = Customer.PhoneNumberHome;
            appUser1.PhoneNumber = Customer.PhoneNumberWork;
            appUser1.PhoneNumber = Customer.PhoneNumberMobile;
            appUser1.Email = Customer.Email;
            appUser1.Address = Customer.Address;
            //appUser1.LockoutEnd = Customer.LockoutEnd;
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser appUser = _userManager.Users.Where(user => user.Id.Equals(id)).FirstOrDefault();
            if (appUser == null)
            {
                return NotFound();
            }
            Customer customer = new Customer();
            ConvertFromAppUserToCustomer(appUser, ref customer);
            return View(ViewPath + "Edit.cshtml", customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            //if (id != category.CategoryId)
            //{
            //    return NotFound();
            //}
            
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser appUser = _userManager.Users.Where(user => user.Id.Equals(customer.CustomerId)).FirstOrDefault();
                    ConvertFromCustomerToAppUser(customer,ref appUser);
                    await _userManager.UpdateAsync(appUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ViewPath + "Edit.cshtml", customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser appUser = _userManager.Users.Where(user => user.Id.Equals(id)).FirstOrDefault();
                
            if (appUser == null)
            {
                return NotFound();
            }
            Customer customer = new Customer();
            ConvertFromAppUserToCustomer(appUser, ref customer);
            return View(ViewPath + "Delete.cshtml", customer);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string CustomerId)
        {
            ApplicationUser appUser = _userManager.Users.Where(user => user.Id.Equals(CustomerId)).FirstOrDefault();
            await _userManager.DeleteAsync(appUser);
            return RedirectToAction(nameof(Index));
        }
    }
}
