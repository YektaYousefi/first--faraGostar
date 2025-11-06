using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PaymentController : Controller
    {
        private readonly FirstProjectContext _context;
        public PaymentController(FirstProjectContext context)
        {

            _context = context;
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Payment()
        {
            var payments = _context.Orders.ToList();
            ViewBag.Payments = payments;
            return View(new Order());
        }
        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult Payment(Order model)
        {

            if (ModelState.IsValid)
            {
                model.PaymentUrl = Guid.NewGuid().ToString();
                _context.Orders.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Payment");
            }
            ViewBag.Payments = _context.Orders.ToList();
            return View(model);
        }
    }
}