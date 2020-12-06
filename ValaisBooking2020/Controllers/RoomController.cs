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


    public class RoomController : Controller
    {

        private IRoomManager RoomManager { get; }
        public IBookingManager BookingManager { get; }

        [TempData]
        public DateTime dateIn { get; set; }
        public DateTime dateOut { get; set; }

        public RoomController(IRoomManager roomManager, IBookingManager bookingManager)
        {
            RoomManager = roomManager;
            BookingManager = bookingManager;
        }

        // GET: RoomController
        [HttpGet]
        public ActionResult Index(SimpleSearchViewModel ssvm)
        {
            dateIn = ssvm.checkIn;
            dateOut = ssvm.checkOut;

            //HttpContext.Session.SetString("location", ssvm.cities.ToString());

            //var room = RoomManager.SearchRoomSimple(ssvm.cities.ToString());
            var list =  BookingManager.GetBookingsWithRoomAndDates(ssvm.checkIn, ssvm.checkOut);
            var roomlist = BookingManager.SearchSimple(list, ssvm.cities.ToString());


            return View(roomlist);
        }


        // GET: RoomController/Details/5
        [HttpPost]
        public ActionResult Index(int id)
        {
            DateTime checkin = (DateTime)TempData.Peek("dateIn");
            DateTime checkout = (DateTime)TempData.Peek("dateOut");
            String city = HttpContext.Session.GetString("location");

            //BookingManager.GetBookingsWithRoomAndDates(id, checkin, checkout);
            var roomlist = BookingManager.SearchSimple(BookingManager.GetBookingsWithRoomAndDates(checkin, checkout), city);

            return RedirectToAction(nameof(Details), roomlist);
        }

        [HttpGet]
        public ActionResult Details(List<DTO.Room> roomlist)
        {
            return View(roomlist);
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
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

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomController/Edit/5
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

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomController/Delete/5
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
