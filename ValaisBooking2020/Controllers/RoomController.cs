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

            
            DateTime firstDateDt = ssvm.checkIn;
            firstDateDt = new DateTime(firstDateDt.Year, firstDateDt.Month, firstDateDt.Day, 0, 0, 0);
            string firstdate = firstDateDt.ToString();
            HttpContext.Session.SetString("firstdate", firstdate);

            DateTime secondDateDt = ssvm.checkOut;
            secondDateDt = new DateTime(secondDateDt.Year, secondDateDt.Month, secondDateDt.Day, 0, 0, 0);
            string seconddate = secondDateDt.ToString();
            HttpContext.Session.SetString("seconddate", seconddate);

            List<Object> criteria = new List<object>();
            string city = ssvm.cities.ToString();
            criteria.Add(city);
            criteria.Add(null);
            criteria.Add(null);
            criteria.Add(null);

            HttpContext.Session.SetString("city", city);

            //recherche de tous les hotels pour une localisation
            var resulthotel = HotelManager.GetHotels();
            var hotel = HotelManager.GetHotelsMultiQueries(criteria, resulthotel);

            return View(hotel);
        }


        // GET: RoomController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            
            HttpContext.Session.SetInt32("idHotel", id);
            string location = HttpContext.Session.GetString("city");

            var date1 = HttpContext.Session.GetString("firstdate");
            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkIn = DateTime.Parse(date1);
            DateTime checkOut = DateTime.Parse(date2);

            var list = BookingManager.GetBookingsWithRoomAndDates(checkIn, checkOut);
            var roomlist = BookingManager.SearchSimple(list, location, id);

            List<DTO.Room> roomlistavailable = new List<DTO.Room>();

                foreach (var room in roomlist)
                {
                    if (room.IdHotel == id)
                    {
                        roomlistavailable.Add(room);
                    }
                }
            int availableRoomsNb = roomlistavailable.Count;
            HttpContext.Session.SetInt32("availableRoomsNb", availableRoomsNb);
            return View(roomlistavailable);
            
        }

        // GET: RoomController/Create
        [HttpGet]
        public ActionResult Picture(int id)
        {
            //récupere toutes les chambres d'un Hotel par rapport l'ID Hotel
            DTO.Room room = RoomManager.SearchRoomById(id);

            //récupéré l'ID Hotel
            var idhotel = HttpContext.Session.GetInt32("idHotel");
            int idHotel = (int)idhotel;

            int idRoom = room.IdRoom;
            HttpContext.Session.SetInt32("idRoom", idRoom);

            //récupere toutes les photos lié à un Id Hotel
            List<DTO.Picture> picture = PictureManager.SearchListPicture(idRoom);

            var date1 = HttpContext.Session.GetString("firstdate");
            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkIn = DateTime.Parse(date1);
            DateTime checkOut = DateTime.Parse(date2);

            //reprend les informations de la list roomlistavailable
            //pour trouver le nombre de chambres encore disponible
            var availableRoomsNb = HttpContext.Session.GetInt32("availableRoomsNb");
            int totalAvailableRooms = (int)availableRoomsNb;

            //calcul du nombre de room total par rapport a l'ID de l'hotel
            List<DTO.Room> totalRoom = RoomManager.GetEveryRoomByIdHotel(idHotel);
            int totalRooms = totalRoom.Count;

            //calclul du prix par rapport au nombre de nuits
            double price = BookingManager.CalculatePrice(room.Price, checkIn, checkOut);

            //majoration du prix par rapport au total des bookings
            double finalPrice = HotelManager.GetExtraPrice(price, totalRooms, totalAvailableRooms);
            string bookingPrice = finalPrice.ToString();
            HttpContext.Session.SetString("bookingPrice", bookingPrice);
        
            var result = new RoomPictureViewModel
            {
                Description = room.Description,
                Price = finalPrice,
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
