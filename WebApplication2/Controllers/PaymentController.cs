using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
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
                // وقتی تازه فرم پر میشه باید رکورد ساخته بشه
                model.PaymentUrl = Guid.NewGuid().ToString();
                model.IsPaid = null; // هنوز پرداخت نشده
                _context.Orders.Add(model);
                _context.SaveChanges();
                Console.WriteLine("Saved Successfully: " + model.CustomerName);

                return RedirectToAction("Payment");
            }

            ViewBag.Payments = _context.Orders.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmPayment(int id)
        {
            //if (ModelState.IsValid)
            //{
            //    model.IsPaid = true;
            //    model.PaymentUrl = Guid.NewGuid().ToString();
            //    _context.Orders.Add(model);
            //    _context.SaveChanges();
            //}
            //return RedirectToAction("Payment");
            var order = _context.Orders.FirstOrDefault(o => o.UserId == id);
            if (order != null)
            {
                order.IsPaid = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Payment");
        }

        [HttpPost]
        public IActionResult CancelPayment(int id)
        {
            //if (ModelState.IsValid)
            //{
            //    model.IsPaid = false; 
            //    model.PaymentUrl = null; 
            //    _context.Orders.Add(model);
            //    _context.SaveChanges();
            //}
            //return RedirectToAction("Payment");

            var order = _context.Orders.FirstOrDefault(o => o.UserId == id);
            if (order != null)
            {
                order.IsPaid = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Payment");
        }

        [HttpGet("Payment/PaymentGateway/{id}")]
        public IActionResult PaymentGateway(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.UserId == id);
            if (order == null)
                return NotFound();

            if (order.IsPaid == true)
                return View("PaymentResult", "پرداخت شما قبلاً انجام شده است ✅");

            if (order.IsPaid == false)
                return View("PaymentResult", "این تراکنش لغو شده است ❌");

            return View(order);
        }

        [HttpPost]
        public IActionResult CompletePayment(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.UserId == id);
            if (order != null)
            {
                order.IsPaid = true;
                _context.SaveChanges();
            }
            return RedirectToAction("PaymentSuccess", new { id });
        }

        public IActionResult PaymentSuccess(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.UserId == id);
            return View(order);
        }


    }
}