using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValaisBooking2020.Models;

namespace ValaisBooking2020.Controllers
{
    public class BookingController : Controller
    {
        // GET: BookingController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Details(BookingViewModel bvm)
        {           
            BookingConfirmationViewModel bcvm = new BookingConfirmationViewModel();

            bcvm.Reference = DateTime.Now.ToString("yyyymmddhhmmss");

            bcvm.Firstname = bvm.Firstname;

            bcvm.Lastname = bvm.Lastname;

            var id = HttpContext.Session.GetInt32("idRoom");
            int idRoom = (int)id;
            bcvm.IdRoom = idRoom;

            var date1 = HttpContext.Session.GetString("firstdate");
            DateTime checkIn = DateTime.Parse(date1);
            bcvm.CheckIn = checkIn;

            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkOut = DateTime.Parse(date2);
            bcvm.CheckOut = checkOut;

            var price = HttpContext.Session.GetString("bookingPrice");
            double bookingPrice = double.Parse(price);
            bcvm.Amount = bookingPrice;


            return RedirectToAction("Details", "Booking", bcvm);
        }

        // GET: BookingController/Details/5
        public ActionResult Details(BookingConfirmationViewModel bcvm)
        {
            return View();
        }

        // GET: BookingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
