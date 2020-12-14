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
    public class BookingGroupController : Controller
    {
        private IBookingManager BookingManager { get; }

        public BookingGroupController(IBookingManager bookingManager)
        {
            BookingManager = bookingManager;
        }
        // GET: BookingGroupController
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookingGroupController/Details/5
        [HttpPost]
        public ActionResult Details(BookingViewModel bvm)
        {
            BookingGroupConfirmationViewModel bcvm = new BookingGroupConfirmationViewModel();

            bcvm.Reference = DateTime.Now.ToString("yyyymmmddhhmmss");

            HttpContext.Session.SetString("reference", bcvm.Reference);

            bcvm.Firstname = bvm.Firstname;
            HttpContext.Session.SetString("firstname", bcvm.Firstname);

            bcvm.Lastname = bvm.Lastname;
            HttpContext.Session.SetString("lastname", bcvm.Lastname);

            /*
            var id = HttpContext.Session.GetInt32("idRoom");
            int idRoom = (int)id;
            bcvm.IdRoom = idRoom;
            */

            var date1 = HttpContext.Session.GetString("firstdate");
            DateTime checkIn = DateTime.Parse(date1);
            bcvm.CheckIn = checkIn;

            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkOut = DateTime.Parse(date2);
            bcvm.CheckOut = checkOut;

            var price = HttpContext.Session.GetString("bookingPrice");
            double bookingPrice = double.Parse(price);
            bcvm.Amount = bookingPrice;

            return RedirectToAction("Details", "BookingGroup", bcvm);
        }

        // GET: BookingGroupController/Create
        public ActionResult Details(BookingGroupConfirmationViewModel bcvm)
        {

            return View(bcvm);
        }

        public ActionResult Confirmation(BookingEndViewModel bevm)
        {
            DTO.Room room = new DTO.Room();
            //List<int> listIdRoom = (List<int>)TempData["ListIdRoom"];
            // object[] test = (object[])TempData["ListIdRoom"];
            //room = (DTO.Room)test[0];
            //listIdRoom = test;
            /*
            string test = HttpContext.Session.GetString("ListIdRoom");
            String[] idRooms = test.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            List<int> IdRoom = idRooms.Select(x=> int.Parse(x)).ToList();
            */
            
            int room1 = (int)HttpContext.Session.GetInt32("room1");
            int room2 = (int)HttpContext.Session.GetInt32("room2");
            int room3 = (int)HttpContext.Session.GetInt32("room3");
            int room4 = (int)HttpContext.Session.GetInt32("room4");
            int room5 = (int)HttpContext.Session.GetInt32("room5");
            int room6 = (int)HttpContext.Session.GetInt32("room6");
            int room7 = (int)HttpContext.Session.GetInt32("room7");
            int room8 = (int)HttpContext.Session.GetInt32("room8");
            int room9 = (int)HttpContext.Session.GetInt32("room9");
            int room10 = (int)HttpContext.Session.GetInt32("room10");
            int room11 = (int)HttpContext.Session.GetInt32("room11");

            List<int> listTemp = new List<int>() { room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11 };
            
            List<int> IdRooms = new List<int>();

            for(int i = 0; i < listTemp.Count(); i++)
            {
                if(listTemp[i] != 0)
                {
                    IdRooms.Add(i);
                }
            }

            int bookingNumber = 0;
            string references = "";
       
                    for (int i = 0; i < IdRooms.Count(); i++)
                    {

                        var reference = DateTime.Now.ToString("yyyyMMddhhmmss");
                        var checkIn = HttpContext.Session.GetString("firstdate");
                        var checkOut = HttpContext.Session.GetString("seconddate");
                        var lastname = HttpContext.Session.GetString("lastname");
                        var firstname = HttpContext.Session.GetString("firstname");
                        var amount = HttpContext.Session.GetString("bookingPrice");
                        var id = listTemp[i];

                        DTO.Booking booking = new DTO.Booking
                        {
                            Reference = (string)reference + bookingNumber,
                            CheckIn = DateTime.Parse(checkIn),
                            CheckOut = DateTime.Parse(checkOut),
                            Lastname = lastname,
                            Firstname = firstname,
                            Amount = double.Parse(amount),
                            IdRoom = (int)id,
                        };

                        List<DTO.Booking> listbooking = BookingManager.GetEveryReservation();
                        int check = 0;
                        foreach (var book in listbooking)
                        {
                            if (book.Reference == reference && book.IdRoom == id)
                            {
                                check = 1;
                            }
                        }
                        if (check == 0)
                        {
                            BookingManager.AddBooking(booking);
                        }

                        bookingNumber++;
                        references += " * " + reference + bookingNumber;
                    }
            

            bevm.confirmationMessage = "Merci de votre réservation! Veuillez conserver votre numéro de réservation: ";
            bevm.referenceNumber = references;

            return View(bevm);
        }
            // POST: BookingGroupController/Create
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

        // GET: BookingGroupController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingGroupController/Edit/5
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

        // GET: BookingGroupController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingGroupController/Delete/5
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
