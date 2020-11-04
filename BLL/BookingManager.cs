using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BookingManager : IBookingManager
    {
        public BookingManager(IConfiguration configuration)
        {
            BookingDB = new BookingsDB(configuration);
        }

        public IBookingsDB BookingDB { get; }

        public Bookings AddBooking(Bookings bookings)
        {
            return BookingDB.AddBooking(bookings);
        }

        public List<Bookings> GetAllReservation()
        {
            return BookingDB.GetAllReservation();
        }
        public List<Bookings> GetAllReservationDate(int IdRoom, DateTime CheckIn, DateTime CheckOut)
        {
            return BookingDB.GetAllReservationDate(IdRoom, CheckIn, CheckOut);
        }

        public List<int> GetAllReservationDateSimple(DateTime CheckIn, DateTime CheckOut)
        {
            var bookingsResult = BookingDB.GetAllReservationDateSimple(CheckIn, CheckOut);

            List<int> listRoomBooked = new List<int>();
            foreach (var booking in bookingsResult)
            {
                listRoomBooked.Add(booking.IdRoom);
            }

            return listRoomBooked;
        }


        public void GetEveryBookings(BookingManager bookingsDBManager)
        {
            //Afficher toutes les réservations
            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get every bookings--");
            var bookingsResult = bookingsDBManager.GetAllReservation();
            foreach (var booking in bookingsResult)
            {
                Console.WriteLine(booking.ShortInfo());
            }
        }

        public List<int> GetBookingsWithRoomAndDates(BookingManager bookingsDBManager, int idRoom, DateTime checkIn, DateTime checkOut)
        {
            //Afficher toutes les réservations sur une chambre pour une période donnée
            //Permet de vérifier si la chambre est réservable ou pas. 

            //Console.WriteLine("------------------------------");
            //Console.WriteLine("--Get every bookings with a date for a specific room--");

            var bookingsResult = bookingsDBManager.GetAllReservationDate(idRoom, checkIn, checkOut);

            List<int> listRoomBooked = new List<int>();
            foreach (var booking in bookingsResult)
            {
                //Console.WriteLine(booking.ShortInfo());
                listRoomBooked.Add(booking.IdRoom);
            }

            //Console.WriteLine("------------------------------");
            //Console.WriteLine("------------------------------");
            //Console.WriteLine("------------------------------");

            return listRoomBooked;
        }

        public void SearchSimple(RoomManager roomDBManager, List<int> listRoomBooked, HotelManager hotelDBManager, PictureManager pictureDBManager, string city)
        {
            Console.WriteLine("--Search simple--");
            var roomResult = roomDBManager.SearchRoomSimple(city);
            int sizeBooked = listRoomBooked.Count;
            List<Room> listFinal = new List<Room>();

            for (int i = 0; i < sizeBooked; i++)
            {
                foreach (var room in roomResult)
                {
                    int bookedRoom = listRoomBooked[i];
                    if (room.IdRoom != bookedRoom)
                    {
                        listFinal.Add(room);
                    }
                }
            }
            foreach (var room in listFinal)
            {

                Console.WriteLine(room.ShortInfo());
                Console.WriteLine();
                var hotelResult = hotelDBManager.SearchHotelSimple(room.IdHotel);
                foreach (var hotel in hotelResult)
                {
                    Console.WriteLine(hotel.ShortInfo());
                    Console.WriteLine();

                    var pictureResult = pictureDBManager.SearchListPicture(room.IdRoom);
                    Console.WriteLine("Pictures : ");
                    foreach (var picture in pictureResult)
                    {
                        Console.WriteLine(picture.Url);
                    }

                    Console.WriteLine();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine();
                }
            }

        }

        public void SearchAdvanced(BookingManager bookingsDBManager, RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, List<Object> listCriteriaRoom, List<Object> listCriteriaHotel,DateTime checkIn,DateTime checkOut)
        {

            Console.WriteLine("--Search advanced--");

            //toutes les chambres occupees pour une periode
            var bookingsResult = bookingsDBManager.GetAllReservationDateSimple(checkIn, checkOut);

            //toutes les chambres disponibles pour une periode       
            var roomAvailableResult = bookingsDBManager.SearchEveryAvailableRooms(roomDBManager, hotelDBManager, pictureDBManager, bookingsResult);

            //Ressort les rooms avec les premieres conditions
            var roomResult = roomDBManager.GetRoomsMultiQueries(listCriteriaRoom, roomAvailableResult);

            //Ressort tous les hotels correspondant aux criteres de rooms
            List<Hotel> hotelsAvailable = new List<Hotel>();
            List<Hotel> hotelsFinal = new List<Hotel>();


            
            foreach (var room in roomResult)
            {
                Hotel hotel = hotelDBManager.SearchHotelById(room.IdHotel);

                hotelsAvailable.Add(hotelDBManager.SearchHotelById(room.IdHotel));
                      
            }


            var test = hotelsAvailable.Select(p => p.IdHotel)
                            .Distinct()
                            .ToList();

            foreach(var t in test)
            {
                hotelsFinal.Add(hotelDBManager.SearchHotelById(t));
            }


            //Ressort tous les hotels correspondant aux criteres de hotels
            //var hotelResult = hotelDBManager.getHotelsMultiQueries(listCriteriaHotel, hotelsAvailable);
            var hotelResult = hotelDBManager.GetHotelsMultiQueries(listCriteriaHotel, hotelsFinal);

            foreach (var hotel in hotelResult)
            {

                Console.WriteLine("Resultat final : ");
                Console.WriteLine(hotel.ShortInfo());
                Console.WriteLine();
                Console.WriteLine("------------------------------");
                Console.WriteLine();
             }
            
        }




        //Retourne la liste des chambres disponibles
        public List<Room> SearchEveryAvailableRooms(RoomManager roomDBManager, HotelManager hotelDBManager, PictureManager pictureDBManager, List<int> listRoomBooked)
        {
            //Console.WriteLine("--Search every available rooms--");
            var roomResult = roomDBManager.SearchEveryRooms();
            int sizeBooked = listRoomBooked.Count;
            List<Room> listFinal = new List<Room>();

            for (int i = 0; i < sizeBooked; i++)
            {
                foreach (var room in roomResult)
                {
                    int bookedRoom = listRoomBooked[i];
                    if (room.IdRoom != bookedRoom)
                    {
                        listFinal.Add(room);
                    }
                }
            }
            return listFinal;
        }
    }
}
