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
        private IPictureManager PictureManager { get; }
        

        [TempData]
        public DateTime dateIn { get; set; }
        public DateTime dateOut { get; set; }
        private List<DTO.Room> bookings { get; set; }
        private string location { get; set; }

        public RoomController(IRoomManager roomManager, IBookingManager bookingManager, IHotelManager hotelManager, IPictureManager pictureManager)
        {
            RoomManager = roomManager;
            BookingManager = bookingManager;
            HotelManager = hotelManager;
            PictureManager = pictureManager;
        }

        // GET: RoomController
        [HttpGet]
        public ActionResult Index(SimpleSearchViewModel ssvm)
        {
            dateIn = ssvm.checkIn;
            dateOut = ssvm.checkOut;

            string firstdate = ssvm.checkIn.ToString();
            HttpContext.Session.SetString("firstdate", firstdate);

            string seconddate = ssvm.checkOut.ToString();
            HttpContext.Session.SetString("seconddate", seconddate);

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
        [HttpGet]
        public ActionResult Details(int id)
        {
                string location = HttpContext.Session.GetString("city");

                var date1 = HttpContext.Session.GetString("firstdate");
                var date2 = HttpContext.Session.GetString("seconddate");
                DateTime checkIn = DateTime.Parse(date1);
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

                return View(roomlistavailable);
            
        }

        // GET: RoomController/Create
        [HttpGet]
        public ActionResult Picture(int id)
        {
            DTO.Room room = RoomManager.SearchRoomById(id);
            List<DTO.Picture> picture = PictureManager.SearchListPicture(id);

            var date1 = HttpContext.Session.GetString("firstdate");
            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkIn = DateTime.Parse(date1);
            DateTime checkOut = DateTime.Parse(date2);

            double price = BookingManager.CalculatePrice(room.Price, checkIn, checkOut);

            var result = new RoomPictureViewModel
            {
                Description = room.Description,
                Price = price,
                Pictures = picture
            };
           
            return View(result);
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
