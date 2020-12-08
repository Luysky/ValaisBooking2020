using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValaisBooking2020.Models;
using BLL;

namespace ValaisBooking2020.Controllers
{
    public class RoomAdvancedController : Controller
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

        public RoomAdvancedController(IRoomManager roomManager, IBookingManager bookingManager, IHotelManager hotelManager, IPictureManager pictureManager)
        {
            RoomManager = roomManager;
            BookingManager = bookingManager;
            HotelManager = hotelManager;
            PictureManager = pictureManager;
        }

        // GET: RoomAdvancedController
        [HttpGet]
        public ActionResult Index(AdvancedSearchViewModel asvm)
        {
            dateIn = asvm.checkIn;
            dateOut = asvm.checkOut;

            string firstdate = asvm.checkIn.ToString();
            HttpContext.Session.SetString("firstdate", firstdate);

            string seconddate = asvm.checkOut.ToString();
            HttpContext.Session.SetString("seconddate", seconddate);

            string city = asvm.cities.ToString();
            HttpContext.Session.SetString("city", city);

            int type = asvm.type;
            HttpContext.Session.SetInt32("type", type);

            bool hasTv = asvm.HasTv;
            string hasTv1 = hasTv.ToString();
            HttpContext.Session.SetString("hasTv", hasTv1);

            bool hasHairDryer = asvm.HasHairDryer;
            string hasHairDryer1 = hasHairDryer.ToString();
            HttpContext.Session.SetString("hasHairDryer", hasHairDryer1);

            int category = asvm.Category;
            bool haswifi = asvm.HasWifi;
            bool hasparking = asvm.HasParking;

            List<Object> criteria = new List<object>();
            criteria.Add(city);
            criteria.Add(category);
            criteria.Add(haswifi);
            criteria.Add(hasparking);

            //recherche de tous les hotels pour une localisation
            var resulthotel = HotelManager.GetHotels();
            var hotel = HotelManager.GetHotelsMultiQueries(criteria, resulthotel);

            return View(hotel);
        }

        // GET: RoomAdvancedController/Details/5
        public ActionResult Details(int id)
        {
            string location = HttpContext.Session.GetString("city");

            var date1 = HttpContext.Session.GetString("firstdate");
            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkIn = DateTime.Parse(date1);
            DateTime checkOut = DateTime.Parse(date2);

            var type = HttpContext.Session.GetInt32("type");
            var hasTv = HttpContext.Session.GetString("hasTv");
            bool hasTv1 = bool.Parse(hasTv); 
            var hasHairDryer = HttpContext.Session.GetString("hasHairDryer");
            bool hasHairDryer1 = bool.Parse(hasHairDryer);

            List<Object> criteria = new List<object>();
            criteria.Add(type);
            criteria.Add(hasTv1);
            criteria.Add(hasHairDryer1);

            //crée une liste de toutes les rooms
            var listAllRooms = RoomManager.SearchEveryRooms();

            //crée une liste des rooms qui correspondent aux critères 
            var roomlist = RoomManager.GetRoomsMultiQueries(criteria,listAllRooms);
            
            //crée une liste d'ID de rooms déjà réservé
            var list = BookingManager.GetBookingsWithRoomAndDates(checkIn, checkOut);

            //crée un liste de rooms disponible qui correspondent aux critères 
            var ListRoomAvailable = RoomManager.GetAvailableRooms(roomlist, list);
            
            List<DTO.Room> roomlistavailable = new List<DTO.Room>();

            //crée une liste de toutes les rooms qui correspondent aux critères et qui sont égale a l'ID de l'hotel choisi
            foreach(var room in ListRoomAvailable)
            {
                if(id != room.IdHotel)
                {
                    continue;
                }
                else
                {
                    roomlistavailable.Add(room);
                }
            }
            return View(roomlistavailable);
        }

        // GET: RoomAdvancedController/Create
        public ActionResult Picture(int id)
        {
            DTO.Room room = RoomManager.SearchRoomById(id);
            List<DTO.Picture> picture = PictureManager.SearchListPicture(id);

            var date1 = HttpContext.Session.GetString("firstdate");
            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkIn = DateTime.Parse(date1);
            DateTime checkOut = DateTime.Parse(date2);

            double price = BookingManager.CalculatePrice(room.Price, checkIn, checkOut);

            List<int> getIdRoomFromBookingList = HotelManager.GetIdRoomFromBookingList(BookingManager.GetAllReservation(), checkIn, checkOut);
            List<int> getHotelFromRoomId = HotelManager.GetHotelFromRoomId(getIdRoomFromBookingList);
            double finalPrice = HotelManager.GetExtraPrice(price, getHotelFromRoomId, HotelManager.GetHotels());

            var result = new RoomPictureViewModel
            {
                Description = room.Description,
                Price = finalPrice,
                Pictures = picture
            };

            return View(result);
        }

        // POST: RoomAdvancedController/Create
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

        // GET: RoomAdvancedController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomAdvancedController/Edit/5
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

        // GET: RoomAdvancedController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomAdvancedController/Delete/5
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
