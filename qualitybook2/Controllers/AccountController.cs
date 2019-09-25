using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qualitybook2.Models;
using qualitybook2.ViewModels;


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
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl) && !_userManager.GetRolesAsync(user).Result.Contains("admin"))
                        //return RedirectToAction("Index","Home");
                        return RedirectToAction("Index", "Profile");
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl) && _userManager.GetRolesAsync(user).Result.Contains("admin"))
                        return RedirectToAction("Index", "Admin/Home");

                    return Redirect(loginViewModel.ReturnUrl);
                    
                }
            }

            if (_userManager.Users.Where(u => u.UserName == loginViewModel.UserName).Count()>0 && _userManager.Users.Where(u=>u.UserName== loginViewModel.UserName).FirstOrDefault().LockoutEnd!=null)
            {
                ModelState.AddModelError("", "The user is locked.");
                return View(loginViewModel);
            }

            ModelState.AddModelError("", "Wrong user name or password. Or try to register a new account.");
            return View(loginViewModel);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = registerViewModel.UserName };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    var phoneNumber = "";
                    var email = "";
                    var address = "";

                    //await _userManager.SetEmailAsync(user,registerViewModel.Email);
                    if (registerViewModel.HomeNumber.Trim()!="")
                    {
                        phoneNumber=registerViewModel.HomeNumber;
                    }
                    else if (registerViewModel.HomeNumber.Trim() == "" && registerViewModel.Mobile.Trim() != "")
                    {
                        phoneNumber=registerViewModel.Mobile;
                    }
                    else if (registerViewModel.HomeNumber.Trim() == "" 
                        && registerViewModel.Mobile.Trim() == "" 
                        && registerViewModel.WorkNumber.Trim()!= "")
                    {
                        phoneNumber=registerViewModel.WorkNumber;
                    }
                    else
                    {
                        phoneNumber= "";
                    }

                    email = registerViewModel.Email;
                    address = registerViewModel.Address;

                    user.Email = email;
                    user.PhoneNumber = phoneNumber;
                    user.Address = address;

                    await _userManager.UpdateAsync(user);

                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Failed : PasswordTooShort,PasswordRequiresNonAlphanumeric,PasswordRequiresLower,PasswordRequiresUpper");
                }
            }
            return View(registerViewModel);
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
