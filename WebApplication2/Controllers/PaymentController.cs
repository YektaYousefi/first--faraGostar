using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly FirstProjectContext _context;
        public PaymentController(FirstProjectContext context)
        {

            _context = context;
        }


        [HttpGet]
        public IActionResult PaymentList(int page = 1)
        {
            //var payments = _context.Orders.ToList();
            //return View("PaymentList", payments);
            int pageSize = 5;
            var allPayments = _context.Orders
                .OrderByDescending(p => p.OrderId)
                .ToList();

            int skip = (page - 1) * pageSize;

            var pagedPayments = allPayments
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPayments = allPayments.Count;
            ViewBag.PaidPayments = allPayments.Count(p => p.IsPaid == true);
            ViewBag.PendingPayments = allPayments.Count(p => p.IsPaid == null);
            ViewBag.TotalAmount = allPayments.Sum(p => p.Amount);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)allPayments.Count / pageSize);

            return View(pagedPayments);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Payment()
        {
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

                return RedirectToAction("PaymentList", "Payment");
            }

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
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.IsPaid = true;
                _context.SaveChanges();
            }
            return RedirectToAction("PaymentList","Payment");

        }

        [HttpPost]
        public IActionResult CancelPayment(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.IsPaid = false;
                _context.SaveChanges();
            }
            return RedirectToAction("PaymentList","Payment");
        }

        [AllowAnonymous]
        [HttpGet("Payment/PaymentGateway/{id}")]
        public IActionResult PaymentGateway(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
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
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                order.IsPaid = true;
                _context.SaveChanges();
            }
            return RedirectToAction("PaymentSuccess", new { id });
        }

        public IActionResult PaymentSuccess(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            return View(order);
        }


    }
}