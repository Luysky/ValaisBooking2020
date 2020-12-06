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
        private IBookingManager BookingManager { get; }
        private IHotelManager HotelManager { get; }
        

        [TempData]
        public DateTime dateIn { get; set; }
        public DateTime dateOut { get; set; }
        private List<DTO.Room> bookings { get; set; }
        private string location { get; set; }

        public RoomController(IRoomManager roomManager, IBookingManager bookingManager, IHotelManager hotelManager)
        {
            RoomManager = roomManager;
            BookingManager = bookingManager;
            HotelManager = hotelManager;
        }

        // GET: RoomController
        [HttpGet]
        public ActionResult Index(SimpleSearchViewModel ssvm)
        {
            dateIn = ssvm.checkIn;
            dateOut = ssvm.checkOut;

            string seconddate = ssvm.checkOut.ToString();
            HttpContext.Session.SetString("seconddate", seconddate);
            //HttpContext.Session.SetString("location", ssvm.cities.ToString());

            //var room = RoomManager.SearchRoomSimple(ssvm.cities.ToString());
            List<Object> criteria = new List<object>();
            string city = ssvm.cities.ToString();
            criteria.Add(city);
            criteria.Add(null);
            criteria.Add(null);
            criteria.Add(null);

            location = city;
            HttpContext.Session.SetString("city", city);

            //recherche de tous les hotels pour une localisation
            var resulthotel = HotelManager.GetHotels();
            var hotel = HotelManager.GetHotelsMultiQueries(criteria, resulthotel);

            //recherche de toutes les rooms non réservé 
            var list =  BookingManager.GetBookingsWithRoomAndDates(ssvm.checkIn, ssvm.checkOut);
            var roomlist = BookingManager.SearchSimple(list, ssvm.cities.ToString());

            bookings = roomlist;

            return View(hotel);
        }


        // GET: RoomController/Details/5
        [HttpPost]
        public ActionResult Room(int id)
        {
            /*
            var date1 = (DateTime) TempData.Peek("dateIn");
            var date2 = (DateTime) TempData.Peek("dateOut");
            var location = (string) TempData.Peek("location");

            var list = BookingManager.GetBookingsWithRoomAndDates(date1, date2);
            var roomlist = BookingManager.SearchSimple(list, location);

            List<DTO.Room> roomlistavailable = new List<DTO.Room>();

            foreach (var room in roomlist)
            {
                if (room.IdHotel == id)
                {
                    roomlistavailable.Add(room);
                }
            }

            return RedirectToAction(nameof(Details), roomlistavailable);
            */
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            //var hotelResult = HotelManager.SearchListHotelById(room.IdHotel);

                //DateTime date1 = new DateTime();
                //DateTime date2 = new DateTime();
                string location = HttpContext.Session.GetString("city");

                var date1 = TempData.Peek("dateIn");
                var date2 = HttpContext.Session.GetString("seconddate");
                DateTime checkIn = (DateTime)date1;
                DateTime checkOut = DateTime.Parse(date2);

                var list = BookingManager.GetBookingsWithRoomAndDates(checkIn, checkOut);
                var roomlist = BookingManager.SearchSimple(list, location);

                List<DTO.Room> roomlistavailable = new List<DTO.Room>();

                foreach (var room in roomlist)
                {
                    if (room.IdHotel == id)
                    {
                        roomlistavailable.Add(room);
                    }
                }

                return RedirectToAction(nameof(Details), roomlistavailable);
            
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
