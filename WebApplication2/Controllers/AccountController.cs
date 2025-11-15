using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private string StaticUsername = "YektaYsi";
        private string StaticPassword = "22832@Ysi";

        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        //public AccountController(
        //    UserManager<User> userManager,
        //    SignInManager<User> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}
        #region Login
        //[Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (login.UserName == StaticUsername && login.Password == StaticPassword)
            {
                ViewBag.isSuccess = true;
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login.UserName)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //return RedirectToAction("Payment", "Payment");

                return RedirectToAction("PaymentList", "Payment");

            }

            ViewBag.ErrorMessage = "نام کاربری/ کلمه عبور اشتباه وارد شده است";
            return View(login);
        }

        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
        #endregion
    }
}