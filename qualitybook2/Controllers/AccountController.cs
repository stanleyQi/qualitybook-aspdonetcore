using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qualitybook2.Models;
using qualitybook2.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace qualitybook2.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl) && !_userManager.GetRolesAsync(user).Result.Contains("admin"))  return RedirectToAction("Index","Home");
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl) && _userManager.GetRolesAsync(user).Result.Contains("admin")) return RedirectToAction("Index", "Admin/Home");

                    return Redirect(loginViewModel.ReturnUrl);
                    
                }
            }

            if (_userManager.Users.Where(u => u.UserName == loginViewModel.UserName).Count()>0 && _userManager.Users.Where(u=>u.UserName== loginViewModel.UserName).FirstOrDefault().LockoutEnd!=null)
            {
                ModelState.AddModelError("", "The user is locked.");
                return View(loginViewModel);
            }

            ModelState.AddModelError("", "Wrong user name or password.");
            return View(loginViewModel);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = loginViewModel.UserName };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Failed : PasswordTooShort,PasswordRequiresNonAlphanumeric,PasswordRequiresLower,PasswordRequiresUpper");
                }
            }
            return View(loginViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
