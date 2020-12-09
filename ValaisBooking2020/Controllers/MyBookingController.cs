using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValaisBooking2020.Models;

namespace ValaisBooking2020.Controllers
{
    public class MyBookingController : Controller
    {

        private IBookingManager BookingManager { get; }

        public MyBookingController(IBookingManager bookingManager)
        {
            BookingManager = bookingManager;
        }

        // GET: MyBookingController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: MyBookingController/Details/5
        [HttpPost]
        public ActionResult Details(BookingViewModel bvm)
        {
            BookingConfirmationViewModel bcvm = new BookingConfirmationViewModel();

            var myBooking = BookingManager.GetMyReservation(bvm.Reference, bvm.Firstname, bvm.Lastname);

            bcvm.Reference = myBooking.Reference;
            bcvm.CheckIn = myBooking.CheckIn;
            bcvm.CheckOut = myBooking.CheckOut;
            bcvm.Firstname = myBooking.Firstname;
            bcvm.Lastname = myBooking.Lastname;
            bcvm.Amount = myBooking.Amount;
            bcvm.IdRoom = myBooking.IdRoom;

            HttpContext.Session.SetString("reference", bcvm.Reference);

            return RedirectToAction("Details", "MyBooking", bcvm);
        }

        public ActionResult Details(BookingConfirmationViewModel bcvm)
        {
            return View(bcvm);
        }
        // GET: MyBookingController/Create
        public ActionResult Confirmation(BookingCancelViewModel bcavm)
        {
            bcavm.confirmationCancelMessage = "Réservation annulée !";

            var reference = HttpContext.Session.GetString("reference");
            BookingManager.DeleteBooking(reference);

            return View(bcavm);
        }

        // POST: MyBookingController/Create
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

        // GET: MyBookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyBookingController/Edit/5
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

        // GET: MyBookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyBookingController/Delete/5
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
