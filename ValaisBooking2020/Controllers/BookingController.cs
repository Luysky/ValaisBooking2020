using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValaisBooking2020.Models;

using System.Text.Json;

namespace ValaisBooking2020.Controllers
{
    public class BookingController : Controller
    {

        
        private IBookingManager BookingManager { get; }

        public BookingController(IBookingManager bookingManager)
        {
            BookingManager = bookingManager;
        }

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

            bcvm.Reference = DateTime.Now.ToString("yyyyMMddhhmmss");

            HttpContext.Session.SetString("reference", bcvm.Reference);

            bcvm.Firstname = bvm.Firstname;
            HttpContext.Session.SetString("firstname", bcvm.Firstname);

            bcvm.Lastname = bvm.Lastname;
            HttpContext.Session.SetString("lastname", bcvm.Lastname);

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
           
            return View(bcvm);
        }

        // GET: BookingController/Confirmation
        public ActionResult Confirmation(BookingEndViewModel bevm)
        {

            var reference = HttpContext.Session.GetString("reference");
            var checkIn = HttpContext.Session.GetString("firstdate");
            var checkOut = HttpContext.Session.GetString("seconddate");
            var lastname = HttpContext.Session.GetString("lastname");
            var firstname = HttpContext.Session.GetString("firstname");
            var amount = HttpContext.Session.GetString("bookingPrice");
            var id = HttpContext.Session.GetInt32("idRoom");

            DTO.Booking booking = new DTO.Booking
            {
                Reference = (string) reference,
                CheckIn = DateTime.Parse(checkIn),
                CheckOut = DateTime.Parse(checkOut),
                Lastname = lastname,
                Firstname = firstname,
                Amount = double.Parse(amount),
                IdRoom = (int) id,
            };


            bevm.confirmationMessage = "Merci de votre réservation! Veuillez conserver votre numéro de réservation: ";
            bevm.referenceNumber = HttpContext.Session.GetString("reference");

            List<DTO.Booking> listbooking = BookingManager.GetEveryReservation();
            int check = 0;
            foreach(var book in listbooking)
            {
                if(book.Reference == reference && book.IdRoom == id)
                {
                    check = 1;
                }
            }
            if (check == 0)
            {
                BookingManager.AddBooking(booking);
            }
   
            return View(bevm);
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
