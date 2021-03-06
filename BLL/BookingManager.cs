﻿using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    public class BookingManager : IBookingManager
    {

        private IHotelManager HotelManager;
        private IRoomManager RoomManager;
       

        public BookingManager(IBookingDB bookingDB, IRoomDB roomDB, IHotelDB hotelDB, IPictureDB pictureDB, IHotelManager hotelManager, IRoomManager roomManager)
        {
            BookingDB = bookingDB;
            RoomDB = roomDB;
            HotelDB = hotelDB;
            PictureDB = pictureDB;

            HotelManager = hotelManager;
            RoomManager = roomManager;
        }

        public IBookingDB BookingDB { get; }
        public IRoomDB RoomDB { get; }
        public IHotelDB HotelDB { get; }
        public IPictureDB PictureDB { get; }


        public double CalculatePrice(double amount,DateTime checkIn,DateTime checkOut)
        {
            double price = amount;
            double nbnight;

            nbnight = (checkOut - checkIn).TotalDays;

            if (nbnight > 1)
            {
                price = (amount * nbnight);
                return price;
            }

            return price;
        }

        public Booking GetMyReservation(string reference, string firstname, string lastname)
        {
            return BookingDB.GetMyReservation(reference, firstname, lastname);
        }

        public List<Booking> GetEveryReservation()
        {
            return BookingDB.GetEveryReservation();
        }


        public Booking AddBooking(Booking booking)
        {
            return BookingDB.AddBooking(booking);
        }

        public List<Booking> GetAllReservation()
        {
            return BookingDB.GetAllReservation();
        }
        public List<Booking> GetAllReservationDate(DateTime CheckIn, DateTime CheckOut)
        {
            return BookingDB.GetAllReservationDate(CheckIn, CheckOut);
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

        public void GetEveryBookings()
        {
            //Afficher toutes les réservations
            Console.WriteLine("------------------------------");
            Console.WriteLine("--Get every bookings--");
            var bookingsResult = BookingDB.GetAllReservation();
            foreach (var booking in bookingsResult)
            {
                Console.WriteLine(booking.ShortInfo());
            }
        }


        public List<int> GetBookingsWithRoomAndDates(DateTime checkIn, DateTime checkOut)
        {
            //Afficher toutes les réservations sur une chambre pour une période donnée
            //Permet de vérifier si la chambre est réservable ou pas. 
            List<Booking> bookingsResult = new List<Booking>();

            DateTime firstDateDt = checkIn;
            firstDateDt = new DateTime(firstDateDt.Year, firstDateDt.Month, firstDateDt.Day, 0, 0, 0);

            DateTime secondDateDt = checkOut;
            secondDateDt = new DateTime(secondDateDt.Year, secondDateDt.Month, secondDateDt.Day, 0, 0, 0);

            bookingsResult = BookingDB.GetAllReservationDate(firstDateDt, secondDateDt);

            List<int> listRoomBooked = new List<int>();

            if (bookingsResult == null)
            {
                return listRoomBooked;
            }
            else
            {
                foreach (var booking in bookingsResult)
                {
                    listRoomBooked.Add(booking.IdRoom);
                }
                return listRoomBooked;
            }
        }
 

        public List<Room> SearchSimple(List<int> listRoomBooked, string city, int id)
        {
            //Methode void pour l instant a modifier ulterieurement pour retourner une liste avec liste room, liste hotel,liste picture

            //Console.WriteLine("--Search simple--");
            var roomResult = RoomDB.SearchRoomSimple(city, id);
            int sizeBooked = listRoomBooked.Count;
            List<Room> listFinal = new List<Room>();
            

            if(listRoomBooked.Count != 0)
            {
                
                List<Room> listRoomB = new List<Room>();
                Room roomValue = new Room();

                foreach(var index in listRoomBooked)
                {
                    roomValue = RoomManager.SearchRoomById(index);
                    listRoomB.Add(roomValue);
                }

                
                List<Room> toDelete = new List<Room>();


                foreach (var room in listRoomB)
                {
                    foreach (var room1 in roomResult)
                    {
                        if (room.IdRoom == room1.IdRoom)
                        {
                            toDelete.Add(room1);
                        }
                    }
                }

                foreach (var r in toDelete) {
                    roomResult.Remove(r);
                }

             
                return roomResult;
            }
            else
            {
                return roomResult;
            }

 
        }

        public void SearchAdvanced(List<Object> listCriteriaRoom, List<Object> listCriteriaHotel,DateTime checkIn,DateTime checkOut)
        {

            Console.WriteLine("--Search advanced--");

            //toutes les chambres occupees pour une periode
            var bookingsResult = GetAllReservationDateSimple(checkIn, checkOut);

            //toutes les chambres disponibles pour une periode       
            var roomAvailableResult = SearchEveryAvailableRooms(bookingsResult);

            //Ressort les rooms avec les premieres conditions
            var roomResult = RoomManager.GetRoomsMultiQueries(listCriteriaRoom, roomAvailableResult);

            //Ressort tous les hotels correspondant aux criteres de rooms
            List<Hotel> hotelsAvailable = new List<Hotel>();
            List<Hotel> hotelsFinal = new List<Hotel>();
          
            foreach (var room in roomResult)
            {
                hotelsAvailable.Add(HotelDB.SearchHotelById(room.IdHotel));                   
            }

            var test = hotelsAvailable.Select(p => p.IdHotel)
                            .Distinct()
                            .ToList();
            
            foreach(var t in test)
            {
                hotelsFinal.Add(HotelDB.SearchHotelById(t));
            }


            //Ressort tous les hotels correspondant aux criteres de hotels
            var hotelResult = HotelManager.GetHotelsMultiQueries(listCriteriaHotel, hotelsFinal);

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
        public List<Room> SearchEveryAvailableRooms(List<int> listRoomBooked)
        {
            var roomResult = RoomDB.SearchEveryRooms();
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

            //Pouvoir modifier une reservartion
            public Booking UpdateBooking(string Reference, DateTime CheckIn, DateTime CheckOut)
        {
            return BookingDB.UpdateBooking(Reference, CheckIn, CheckOut);
        }

        public int DeleteBooking(string Reference)
        {
            return BookingDB.DeleteBooking(Reference);
        }
    }
}
