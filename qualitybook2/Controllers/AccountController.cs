using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qualitybook2.Extensions;
using qualitybook2.Models;
using qualitybook2.ViewModels;


namespace qualitybook2.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
                    if (user.LockoutEnabled == false)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account is currently Disabled, please consult the Administrator.");
                        return View(loginViewModel);
                    }


                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty,
                                      "You must have a confirmed email to log in.");
                        return View(loginViewModel);
                    }

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

                    //sendemail for confirmation
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    try
                    {
                        await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    

                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Failed : Retry your password setting.");
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}
