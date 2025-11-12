using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private string StaticUsername = "YektaYsi";
        private string StaticPassword = "22832@Ysi";


        #region Login
        //[Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }
            if (login.UserName == StaticUsername && login.Password == StaticPassword)
            {
                ViewBag.isSuccess = true;
                //return RedirectToAction("Payment", "Payment");
                return View(login);

            }
            ViewBag.ErrorMessage = "نام کاربری/ کلمه عبور اشتباه وارد شده است";
            return View(login);
        }

        #endregion
    }
}