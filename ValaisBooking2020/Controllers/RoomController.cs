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
        private List<int> ListIdRoom { get; set; }

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

            //ces deux méthodes permettent de connaitre les chambres encore disponible
            var list = BookingManager.GetBookingsWithRoomAndDates(checkIn, checkOut);
            var roomlist = BookingManager.SearchSimple(list, location, id);

            List<DTO.Room> roomlistavailable = new List<DTO.Room>();

            //création de la vue des chambres de l'hotel
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

        public ActionResult GroupSelection(int id)
        {
            //enregistre l'id de l'hotel
            HttpContext.Session.SetInt32("idHotel", id);
            string location = HttpContext.Session.GetString("city");

            //récupere le checkin et le checkout
            var date1 = HttpContext.Session.GetString("firstdate");
            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkIn = DateTime.Parse(date1);
            DateTime checkOut = DateTime.Parse(date2);

            //ces deux méthodes permettent de connaitre les chambres encore disponible
            var list = BookingManager.GetBookingsWithRoomAndDates(checkIn, checkOut);
            var roomlist = BookingManager.SearchSimple(list, location, id);

            List<RoomGroupViewModel> roomlistavailable = new List<RoomGroupViewModel>();
            
            //création de la vue des chambres de l'hotel
            foreach (var room in roomlist)
            {
                if (room.IdHotel == id)
                {
                    RoomGroupViewModel rgvm = new RoomGroupViewModel();
                    rgvm.IdRoom = room.IdRoom;
                    rgvm.Number = room.Number;
                    rgvm.Description = room.Description;
                    rgvm.Type = room.Type;
                    rgvm.Price = room.Price;
                    rgvm.HasTV = room.HasTV;
                    rgvm.HasHairDryer = room.HasHairDryer;
                    rgvm.IsCheck = false;
                    rgvm.IdHotel = room.IdHotel;
                    roomlistavailable.Add(rgvm);
                }
            }

            //enregistre le nombre de chambre réservable
            int availableRoomsNb = roomlistavailable.Count;
            HttpContext.Session.SetInt32("availableRoomsNb", availableRoomsNb);
            return View(roomlistavailable);

        }

        public ActionResult GroupDetails(int[] selectedRoom)
        {
            //TempData["ListIdRoom"] = selectedRoom;

            /*
             *Nous n'avons pas trouvé de moyen de transferer une liste 
             *d'un controleur à un autre. 
             *La seul solution que nous avons trouvé, est d'envoyer 
             *séparement chaque élèment de la liste via les context
             *
             *Au maximum il peut y a voir 11 chambres réservable dans un hotel
             */
         
            List<int> listRooms = new List<int>();
            listRooms = selectedRoom.ToList();

            
            int room1 = 0;
            int room2 = 0;
            int room3 = 0;
            int room4 = 0;
            int room5 = 0;
            int room6 = 0;
            int room7 = 0;
            int room8 = 0;
            int room9 = 0;
            int room10 = 0;
            int room11 = 0;

            if(selectedRoom.Length >= 1)
            {
                room1 = selectedRoom[0];
            }
            if (selectedRoom.Length >= 2)
            {
                room2 = selectedRoom[1];
            }
            if (selectedRoom.Length >= 3)
            {
                room3 = selectedRoom[2];
            }
            if (selectedRoom.Length >= 4)
            {
                room4 = selectedRoom[3];
            }
            if (selectedRoom.Length >= 5)
            {
                room5 = selectedRoom[4];
            }
            if (selectedRoom.Length >= 6)
            {
                room6 = selectedRoom[5];
            }
            if (selectedRoom.Length >= 7)
            {
                room7 = selectedRoom[6];
            }
            if (selectedRoom.Length >= 8)
            {
                room8 = selectedRoom[7];
            }
            if (selectedRoom.Length >= 9)
            {
                room9 = selectedRoom[8];
            }
            if (selectedRoom.Length >= 10)
            {
                room10 = selectedRoom[9];
            }
            if (selectedRoom.Length >= 11)
            {
                room11 = selectedRoom[10];
            }


            /*
            string[] ListIdRoom = selectedRoom.Select(x => x.ToString()).ToArray();
            string result = string.Join(",", ListIdRoom);
            */

            HttpContext.Session.SetInt32("room1", room1);
            HttpContext.Session.SetInt32("room2", room2);
            HttpContext.Session.SetInt32("room3", room3);
            HttpContext.Session.SetInt32("room4", room4);
            HttpContext.Session.SetInt32("room5", room5);
            HttpContext.Session.SetInt32("room6", room6);
            HttpContext.Session.SetInt32("room7", room7);
            HttpContext.Session.SetInt32("room8", room8);
            HttpContext.Session.SetInt32("room9", room9);
            HttpContext.Session.SetInt32("room10", room10);
            HttpContext.Session.SetInt32("room11", room11);

            List<DTO.Room> listRoom = new List<DTO.Room>();
            
            //Récupération des rooms par appoprt au ID stocké en context
            foreach(var roomId in selectedRoom)
            {
                DTO.Room room = new DTO.Room();
                room = RoomManager.SearchRoomById(roomId);
                listRoom.Add(room);
            }

            //récupere le checkin et le checkout
            var date1 = HttpContext.Session.GetString("firstdate");
            var date2 = HttpContext.Session.GetString("seconddate");
            DateTime checkIn = DateTime.Parse(date1);
            DateTime checkOut = DateTime.Parse(date2);

            //reprend les informations de la list roomlistavailable
            //pour trouver le nombre de chambres encore disponible
            var availableRoomsNb = HttpContext.Session.GetInt32("availableRoomsNb");
            int totalAvailableRooms = (int)availableRoomsNb;

            //récupéré l'ID Hotel
            var idhotel = HttpContext.Session.GetInt32("idHotel");
            int idHotel = (int)idhotel;

            DTO.Hotel hotel = new DTO.Hotel();
            
            hotel = HotelManager.SearchHotelById(idHotel);

            //calcul du nombre de room total par rapport a l'ID de l'hotel
            List<DTO.Room> totalRoom = RoomManager.GetEveryRoomByIdHotel(idHotel);
            int totalRooms = totalRoom.Count;

            double price = 0;


            foreach(var room in listRoom)
            {
                //calclul du prix par rapport au nombre de nuits
                double roomPrice = BookingManager.CalculatePrice(room.Price, checkIn, checkOut);

                //majoration du prix par rapport au total des bookings
                double finalPrice = HotelManager.GetExtraPrice(roomPrice, totalRooms, totalAvailableRooms);

                price += finalPrice;
            }

            //enregistre le prix finale pour la réservation complète 
            string bookingPrice = price.ToString();
            HttpContext.Session.SetString("bookingPrice", bookingPrice);

            RoomGroupDetailsViewModel rgdvm = new RoomGroupDetailsViewModel();

            rgdvm.Name = hotel.Name;
            rgdvm.nbRoom = selectedRoom.Count();
            rgdvm.Price = price;

            return View(rgdvm);
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
